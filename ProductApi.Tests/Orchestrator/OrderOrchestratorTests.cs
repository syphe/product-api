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

            var account = new Account
            {
                Id = Guid.NewGuid(),
            };

            var order = new Order
            {
                Id = Guid.NewGuid(),
                AccountId = account.Id,
            };

            var orderOrchestrator = new OrderOrchestrator();
            orderOrchestrator.CreateOrder(order);

            Assert.NotNull(insertedOrder);
        }

        [Fact]
        public void Test_CreateOrder_WhenCustomerHasOutstandingOrders_ThrowsException()
        {
            var deliveryAddress = "5 Fake Street, Milford, Cork, Ireland";

            var account = new Account
            {
                Id = Guid.NewGuid(),
            };

            var product = new Product
            {
                Id = Guid.NewGuid(),
            };

            // Add some orders to the repository for the customer in which the total exceeds 100 euros.
            var orders = new List<Order>()
            {
                new Order { Id = Guid.NewGuid(), DeliveryAddress = deliveryAddress, AccountId = account.Id, ProductId = product.Id, Quantity = 1, UnitPrice = 50.0M },
                new Order { Id = Guid.NewGuid(), DeliveryAddress = deliveryAddress, AccountId = account.Id, ProductId = product.Id, Quantity = 1, UnitPrice = 50.0M },
                new Order { Id = Guid.NewGuid(), DeliveryAddress = deliveryAddress, AccountId = account.Id, ProductId = product.Id, Quantity = 1, UnitPrice = 25.0M },
            };

            // Assert that the total value exceeds 150 euro.
            Assert.True(orders.Sum(x => x.Quantity * x.UnitPrice) > 150.0M);

            var mockOrderRepository = new Mock<IRepository<Order>>();
            mockOrderRepository.Setup(x => x.GetAll()).Returns(() => orders.AsQueryable());

            var orderOrchestrator = new OrderOrchestrator();

            Assert.Throws<CustomerHasOutstandingOrdersException>(() =>
            {
                orderOrchestrator.CreateOrder(new Order { Id = Guid.NewGuid(), DeliveryAddress = deliveryAddress, AccountId = account.Id, ProductId = product.Id, Quantity = 1, UnitPrice = 25.0M });
            });
        }

        [Fact]
        public void Test_CreateOrder_WhenProductAlreadyBackOrdered_ThrowsException()
        {
            var deliveryAddress = "63 Transient Crescent, Whitegate, Cork, Ireland";

            var account = new Account
            {
                Id = Guid.NewGuid(),
            };

            var product = new Product
            {
                Id = Guid.NewGuid(),
            };

            // Add some orders to the repository for the customer in which the total quantity exceeds 10.
            var orders = new List<Order>()
            {
                new Order { Id = Guid.NewGuid(), DeliveryAddress = deliveryAddress, AccountId = account.Id, ProductId = product.Id, Quantity = 1, UnitPrice = 50.0M },
                new Order { Id = Guid.NewGuid(), DeliveryAddress = deliveryAddress, AccountId = account.Id, ProductId = product.Id, Quantity = 1, UnitPrice = 50.0M },
                new Order { Id = Guid.NewGuid(), DeliveryAddress = deliveryAddress, AccountId = account.Id, ProductId = product.Id, Quantity = 1, UnitPrice = 25.0M },
            };

            // Just quickly assert that the order collection above has the right conditions.
            Assert.True(orders.Sum(x => x.Quantity) > 10);
            
            var mockOrderRepository = new Mock<IRepository<Order>>();
            mockOrderRepository.Setup(x => x.GetAll()).Returns(() => orders.AsQueryable());

            var orderOrchestrator = new OrderOrchestrator();

            Assert.Throws<ProductBackOrderedException>(() =>
            {
                orderOrchestrator.CreateOrder(new Order { Id = Guid.NewGuid(), DeliveryAddress = deliveryAddress, AccountId = account.Id, ProductId = product.Id, Quantity = 1, UnitPrice = 25.0M });
            });
        }

        [Fact]
        public void Test_CreateOrder_WhenUnitPriceLowerThanCostPrice_ThrowsException()
        {
            var account = new Account
            {
                Id = Guid.NewGuid(),
            };

            var product = new Product
            {
                Id = Guid.NewGuid(),
                CostPrice = 10.0M,
            };

            var order = new Order
            {
                Id = Guid.NewGuid(),
                AccountId = account.Id,
                ProductId = product.Id,
                UnitPrice = 8.0M
            };

            Assert.True(order.UnitPrice < product.CostPrice);

            var orderOrchestrator = new OrderOrchestrator();

            Assert.Throws<SalePriceLowerThanCostPriceException>(() =>
            {
                orderOrchestrator.CreateOrder(order);
            });
        }
    }
}
