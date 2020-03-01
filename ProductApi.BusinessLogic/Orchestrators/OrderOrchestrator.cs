using System;
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
            var product = _productRepository.GetById(order.ProductId);
            if (product == null)
            {
                throw new ProductNotFoundException(order.ProductId);
            }

            if (order.UnitPrice < product.CostPrice)
            {
                throw new SalePriceLowerThanCostPriceException(order.UnitPrice, product.CostPrice);
            }
        }
    }
}
