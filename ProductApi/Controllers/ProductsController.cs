using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductApi.BusinessLogic.Orchestrators;
using ProductApi.Model.Entities;

namespace ProductApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductOrchestrator _productOrchestrator;

        public ProductsController(IProductOrchestrator productOrchestrator)
        {
            _productOrchestrator = productOrchestrator;
        }

        [HttpGet("accounts/{accountId}/products")]
        public ActionResult<IEnumerable<Product>> GetAll(Guid accountId)
        {
            var products = _productOrchestrator.GetAll(accountId);
            return Ok(products);
        }
    }
}
