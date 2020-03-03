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

        /// <summary>
        /// Gets all the Orders for a given Account.
        /// </summary>
        /// <param name="accountId">The Id of the Account to retrieve Orders for.</param>
        /// <returns>The collection of Orders which belong to this Account.</returns>
        IEnumerable<Order> GetAll(Guid accountId);

        /// <summary>
        /// Gets a single Order given an AccountId and OrderId.
        /// </summary>
        /// <param name="accountId">The AccountId the Order belongs to.</param>
        /// <param name="id">The Id of the Order to retrieve.</param>
        /// <returns>The Order requested.</returns>
        Order GetById(Guid accountId, Guid id);
    }
}
