using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProductApi.BusinessLogic.Orchestrators;
using ProductApi.Model.Entities;

namespace ProductApi.Controllers
{
    /// <summary>
    /// Controller for operations relating to customer Orders.
    /// </summary>
    [Route("api")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderOrchestrator _orderOrchestrator;

        /// <summary>
        /// Initializes a new instance of the OrdersController class.
        /// </summary>
        /// <param name="orderOrchestrator">The <see cref="IOrderOrchestrator"/> required for operations.</param>
        public OrdersController(IOrderOrchestrator orderOrchestrator)
        {
            _orderOrchestrator = orderOrchestrator;
        }

        /// <summary>
        /// Gets all the Orders for a given Account.
        /// </summary>
        /// <param name="accountId">The Id of the Account to retrieve Orders for.</param>
        /// <returns>The collection of Orders which belong to this Account.</returns>
        [HttpGet("accounts/{accountId}/orders")]
        public ActionResult<IEnumerable<Order>> GetAll(Guid accountId)
        {
            var orders = _orderOrchestrator.GetAll(accountId);
            return Ok(orders);
        }

        /// <summary>
        /// Gets a single Order given an AccountId and OrderId.
        /// </summary>
        /// <param name="accountId">The AccountId the Order belongs to.</param>
        /// <param name="id">The Id of the Order to retrieve.</param>
        /// <returns>The Order requested.</returns>
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

        /// <summary>
        /// Creates an Order for the given Account.
        /// </summary>
        /// <param name="accountId">The AccountId the Order should belong to.</param>
        /// <param name="order">The Order to create.</param>
        /// <returns>The created Order, with id's and other properties filled in.</returns>
        [HttpPost("accounts/{accountId}/orders")]
        public ActionResult<Order> CreateOrder(Guid accountId, [FromBody]Order order)
        {
            if (accountId != order.AccountId)
            {
                return BadRequest("AccountId does not match the Order passed in.");
            }

            try
            {
                _orderOrchestrator.CreateOrder(order);
                return Ok(order);
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
