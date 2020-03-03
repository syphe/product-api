using System;
using System.Collections.Generic;
using System.Text;
using ProductApi.Model.Entities;

namespace ProductApi.BusinessLogic.Orchestrators
{
    /// <summary>
    /// Defines operations relating to the Products entity.
    /// </summary>
    public interface IProductOrchestrator
    {
        /// <summary>
        /// Gets all the Products in the system for the specified Account.
        /// </summary>
        /// <param name="accountId">The AccountId of the Account to get products for.</param>
        /// <returns>The collection of all Products for the specified Account.</returns>
        IEnumerable<Product> GetAll(Guid accountId);
    }
}
