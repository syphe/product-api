using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProductApi.BusinessLogic.Orchestrators;
using ProductApi.DataAccess;
using ProductApi.Model.Entities;

namespace ProductApi.Controllers
{
    /// <summary>
    /// Controller for operations relating to customer Accounts.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountOrchestrator _accountOrchestrator;

        /// <summary>
        /// Initializes a new instance of the AccountsController class.
        /// </summary>
        /// <param name="accountOrchestrator">The <see cref="IAccountOrchestrator"/> required for performing operations.</param>
        public AccountsController(IAccountOrchestrator accountOrchestrator)
        {
            _accountOrchestrator = accountOrchestrator;
        }
        
        /// <summary>
        /// Gets all the Accounts from the system.
        /// </summary>
        /// <returns>A collection of all the Accounts added to the system.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Account>> GetAll()
        {
            var accounts = _accountOrchestrator.GetAll();
            return Ok(accounts);
        }

        /// <summary>
        /// Gets a single Account with the specified Id.
        /// </summary>
        /// <param name="id">The Id of an Account to retrieve.</param>
        /// <returns>An Account with the specified Id.</returns>
        [HttpGet("{id}")]
        public ActionResult<Account> Get(Guid id)
        {
            var account = _accountOrchestrator.GetById(id);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        /// <summary>
        /// Inserts a new Account into the system.
        /// </summary>
        /// <param name="account">The Account to add.</param>
        /// <returns>The resulting Account object if successful, BadRequest otherwise.</returns>
        [HttpPost]
        public ActionResult<Account> CreateAccount([FromBody] Account account)
        {
            try
            {
                _accountOrchestrator.CreateAccount(account);
                return Ok(account);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Deletes an Account from the system.
        /// </summary>
        /// <param name="id">The Id of the Account to delete.</param>
        /// <returns>An Http Ok if the delete was successful, BadRequest otherwise.</returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            try
            {
                _accountOrchestrator.DeleteAccount(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
