using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductApi.BusinessLogic.Orchestrators;
using ProductApi.Model.Entities;

namespace ProductApi.Controllers
{
    /// <summary>
    /// Controller for operations relating to the Products entity.
    /// </summary>
    [Route("api")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductOrchestrator _productOrchestrator;

        /// <summary>
        /// Initializes a new instance of the ProductsController class.
        /// </summary>
        /// <param name="productOrchestrator">The <see cref="IProductOrchestrator"/> required for operations.</param>
        public ProductsController(IProductOrchestrator productOrchestrator)
        {
            _productOrchestrator = productOrchestrator;
        }

        /// <summary>
        /// Gets all the Products in the system for the specified Account.
        /// </summary>
        /// <param name="accountId">The AccountId of the Account to get products for.</param>
        /// <returns>The collection of all Products for the specified Account.</returns>
        [HttpGet("accounts/{accountId}/products")]
        public ActionResult<IEnumerable<Product>> GetAll(Guid accountId)
        {
            var products = _productOrchestrator.GetAll(accountId);
            return Ok(products);
        }
    }
}
