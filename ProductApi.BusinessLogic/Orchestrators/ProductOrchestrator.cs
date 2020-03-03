using System;
using System.Collections.Generic;
using System.Linq;
using ProductApi.DataAccess;
using ProductApi.Model.Entities;

namespace ProductApi.BusinessLogic.Orchestrators
{
    /// <summary>
    /// Implementatoin of the <see cref="IProductOrchestrator"/> interface.
    /// </summary>
    internal class ProductOrchestrator : IProductOrchestrator
    {
        private readonly IRepository<Product> _productRepository;

        /// <summary>
        /// Initializes a new instance of the ProductOrchestrator class.
        /// </summary>
        /// <param name="productRepository">The <see cref="IRepository{T}"/> required for Products.</param>
        public ProductOrchestrator(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        /// <inheritdoc/>
        public IEnumerable<Product> GetAll(Guid accountId)
        {
            var products = _productRepository.GetAll().Where(x => x.AccountId == accountId);
            return products;
        }
    }
}
