using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ProductApi.DataAccess;
using ProductApi.Model.Entities;
using ProductApi.Model.Exceptions;

namespace ProductApi.BusinessLogic.Orchestrators
{
    /// <summary>
    /// Implementation of the <see cref="IOrderOrchestrator"/> interface.
    /// </summary>
    public class OrderOrchestrator : IOrderOrchestrator
    {
        private const decimal OutstandingOrderMaxValue = 150.0M;
        private const int BackOrderLimit = 10;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Product> _productRepository;

        public OrderOrchestrator(
            IRepository<Order> orderRepository,
            IRepository<Product> productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        /// <inheritdoc/>
        public void CreateOrder(Order order)
        {
            // Product is not found if the id doesn't exist, or if the product exists for a different account.
            var product = _productRepository.GetById(order.ProductId);
            if (product == null || product.AccountId != order.AccountId)
            {
                throw new ProductNotFoundException(order.ProductId);
            }

            if (order.UnitPrice < product.CostPrice)
            {
                throw new SalePriceLowerThanCostPriceException(order.UnitPrice, product.CostPrice);
            }

            var outstandingOrdersValue = _orderRepository.GetAll()
                .Where(x => x.AccountId == order.AccountId && !x.Paid && x.DeliveryAddress == order.DeliveryAddress)
                .Sum(x => x.Quantity * x.UnitPrice);

            if (outstandingOrdersValue > OutstandingOrderMaxValue)
            {
                throw new CustomerHasOutstandingOrdersException(OutstandingOrderMaxValue);
            }

            var numBackOrdered = _orderRepository.GetAll()
                .Where(x => x.AccountId == order.AccountId && x.ProductId == order.ProductId && !x.Complete)
                .Sum(x => x.Quantity);

            if (numBackOrdered > BackOrderLimit)
            {
                throw new ProductBackOrderedException();
            }

            _orderRepository.Insert(order);
        }

        /// <inheritdoc/>
        public IEnumerable<Order> GetAll(Guid accountId)
        {
            var orders = _orderRepository.GetAll().Where(x => x.AccountId == accountId);
            return orders;
        }

        /// <inheritdoc/>
        public Order GetById(Guid accountId, Guid id)
        {
            var order = _orderRepository.GetById(id);

            if (order.AccountId != accountId)
            {
                return null;
            }

            return order;
        }
    }
}
