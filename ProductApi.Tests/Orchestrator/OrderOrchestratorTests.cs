using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using ProductApi.BusinessLogic.Orchestrators;
using ProductApi.DataAccess;
using ProductApi.Model.Entities;
using ProductApi.Model.Exceptions;
using Xunit;

namespace ProductApi.Tests.Orchestrator
{
    public class OrderOrchestratorTests
    {
        [Fact]
        public void Test_CreateOrder_WithValidOrder_OrderInsertedIntoRepository()
        {
            Order insertedOrder = null;
            var mockOrderRepository = new Mock<IRepository<Order>>();
            mockOrderRepository.Setup(x => x.Insert(It.IsAny<Order>())).Callback<Order>(x => insertedOrder = x);

            var account = new Account("Test Account");

            var product = new Product(account.Id, "Test Product", 15.0M);

            var order = new Order(account.Id, product.Id, 1, 20.0M, "4 Other Street, Another Suburb, A City, That Country");

            var orderOrchestrator = new OrderOrchestrator();
            orderOrchestrator.CreateOrder(order);

            Assert.NotNull(insertedOrder);
        }

        [Fact]
        public void Test_CreateOrder_WhenCustomerHasOutstandingOrders_ThrowsException()
        {
            var deliveryAddress = "5 Fake Street, Milford, Cork, Ireland";

            var account = new Account("Test Account");

            var product = new Product(account.Id, "Test Product", 15.0M);

            // Add some orders to the repository for the customer in which the total exceeds 100 euros.
            var orders = new List<Order>()
            {
                new Order(account.Id, product.Id, 1, 50.0M, deliveryAddress),
                new Order(account.Id, product.Id, 1, 50.0M, deliveryAddress),
                new Order(account.Id, product.Id, 1, 25.0M, deliveryAddress),
            };

            // Assert that the total value exceeds 150 euro.
            Assert.True(orders.Sum(x => x.Quantity * x.UnitPrice) > 150.0M);

            var mockOrderRepository = new Mock<IRepository<Order>>();
            mockOrderRepository.Setup(x => x.GetAll()).Returns(() => orders.AsQueryable());

            var orderOrchestrator = new OrderOrchestrator();

            Assert.Throws<CustomerHasOutstandingOrdersException>(() =>
            {
                orderOrchestrator.CreateOrder(new Order(account.Id, product.Id, 1, 25.0M, deliveryAddress));
            });
        }

        [Fact]
        public void Test_CreateOrder_WhenProductAlreadyBackOrdered_ThrowsException()
        {
            var deliveryAddress = "63 Transient Crescent, Whitegate, Cork, Ireland";

            var account = new Account("Test Account");

            var product = new Product(account.Id, "Test Product", 15.0M);

            // Add some orders to the repository for the customer in which the total quantity exceeds 10.
            var orders = new List<Order>()
            {
                new Order(account.Id, product.Id, 1, 50.0M, deliveryAddress),
                new Order(account.Id, product.Id, 1, 50.0M, deliveryAddress),
                new Order(account.Id, product.Id, 1, 25.0M, deliveryAddress),
            };

            // Just quickly assert that the order collection above has the right conditions.
            Assert.True(orders.Sum(x => x.Quantity) > 10);
            
            var mockOrderRepository = new Mock<IRepository<Order>>();
            mockOrderRepository.Setup(x => x.GetAll()).Returns(() => orders.AsQueryable());

            var orderOrchestrator = new OrderOrchestrator();

            Assert.Throws<ProductBackOrderedException>(() =>
            {
                orderOrchestrator.CreateOrder(new Order(account.Id, product.Id, 1, 25.0M, deliveryAddress));
            });
        }

        [Fact]
        public void Test_CreateOrder_WhenUnitPriceLowerThanCostPrice_ThrowsException()
        {
            var account = new Account("Test Account");

            var product = new Product(account.Id, "Test Product", 10.0M);

            var order = new Order(account.Id, product.Id, 1, 8.0M, "65 Fake Street, Some Suburb, Some City, Some Country");

            Assert.True(order.UnitPrice < product.CostPrice);

            var orderOrchestrator = new OrderOrchestrator();

            Assert.Throws<SalePriceLowerThanCostPriceException>(() =>
            {
                orderOrchestrator.CreateOrder(order);
            });
        }
    }
}
