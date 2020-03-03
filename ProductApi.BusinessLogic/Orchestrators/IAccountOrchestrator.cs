using System;
using System.Collections.Generic;
using System.Text;
using ProductApi.Model.Entities;

namespace ProductApi.BusinessLogic.Orchestrators
{
    /// <summary>
    /// Defines operations relating to the Account entity.
    /// </summary>
    public interface IAccountOrchestrator
    {
        /// <summary>
        /// Gets all the Accounts from the system.
        /// </summary>
        /// <returns>A collection of all the Accounts added to the system.</returns>
        IEnumerable<Account> GetAll();

        /// <summary>
        /// Gets a single Account with the specified Id.
        /// </summary>
        /// <param name="id">The Id of an Account to retrieve.</param>
        /// <returns>An Account with the specified Id.</returns>
        Account GetById(Guid id);

        /// <summary>
        /// Inserts a new Account into the system.
        /// </summary>
        /// <param name="account">The Account to add.</param>
        /// <returns>The resulting Account object if successful, BadRequest otherwise.</returns>
        void CreateAccount(Account account);

        /// <summary>
        /// Deletes an Account from the system.
        /// </summary>
        /// <param name="id">The Id of the Account to delete.</param>
        /// <returns>An Http Ok if the delete was successful, BadRequest otherwise.</returns>
        void DeleteAccount(Guid id);
    }
}
