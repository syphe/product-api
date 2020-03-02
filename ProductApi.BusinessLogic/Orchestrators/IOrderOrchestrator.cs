using System;
using System.Collections.Generic;
using ProductApi.Model.Entities;

namespace ProductApi.BusinessLogic.Orchestrators
{
    /// <summary>
    /// The <see cref="IOrderOrchestrator"/> interface defines methods that apply to Orders within the system.
    /// </summary>
    public interface IOrderOrchestrator
    {
        /// <summary>
        /// Creates an order within the system with the given parameter.
        /// </summary>
        /// <param name="order">The Order to create, if the order is valid, nothing is returned, otherwise an Exception is thrown.</param>
        void CreateOrder(Order order);

        IEnumerable<Order> GetAll(Guid accountId);

        Order GetById(Guid accountId, Guid id);
    }
}
