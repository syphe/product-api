using System;
using System.Collections.Generic;
using System.Linq;
using ProductApi.DataAccess;
using ProductApi.Model.Entities;

namespace ProductApi.BusinessLogic.Orchestrators
{
    internal class ProductOrchestrator : IProductOrchestrator
    {
        private readonly IRepository<Product> _productRepository;

        public ProductOrchestrator(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<Product> GetAll(Guid accountId)
        {
            var products = _productRepository.GetAll().Where(x => x.AccountId == accountId);
            return products;
        }
    }
}
