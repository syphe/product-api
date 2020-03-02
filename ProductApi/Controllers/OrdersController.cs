using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProductApi.BusinessLogic.Orchestrators;
using ProductApi.Model.Entities;

namespace ProductApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderOrchestrator _orderOrchestrator;

        public OrdersController(IOrderOrchestrator orderOrchestrator)
        {
            _orderOrchestrator = orderOrchestrator;
        }

        [HttpGet("accounts/{accountId}/orders")]
        public ActionResult<IEnumerable<Order>> GetAll(Guid accountId)
        {
            var orders = _orderOrchestrator.GetAll(accountId);
            return Ok(orders);
        }

        [HttpGet("accounts/{accountId}/orders/{id}")]
        public ActionResult<Order> GetById(Guid accountId, Guid id)
        {
            var order = _orderOrchestrator.GetById(accountId, id);

            if (order != null)
            {
                return Ok(order);
            }

            return NotFound();
        }

        [HttpPost("accounts/{accountId}/orders")]
        public ActionResult<Guid> CreateOrder(Guid accountId, [FromBody]Order order)
        {
            if (accountId != order.AccountId)
            {
                return BadRequest("AccountId does not match the Order passed in.");
            }

            try
            {
                _orderOrchestrator.CreateOrder(order);
                return Ok(order.Id);
            }
            catch (Exception ex)
            {
                // In a Production environment, would not want to give any consumers of this API detailed error messages,
                // but for Demo purposes it should be ok.
                return StatusCode(500, ex.Message);
            }
        }
    }
}
