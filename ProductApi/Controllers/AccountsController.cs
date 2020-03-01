using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductApi.DataAccess;
using ProductApi.DataAccess.Entities;

namespace ProductApi.Controllers
{
    /// <summary>
    /// Controller for operations relating to customer Accounts.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IRepository<Account> _accountRepository;

        /// <summary>
        /// Initializes a new instance of the AccountsController class.
        /// </summary>
        /// <param name="accountRepository">The <see cref="IRepository{T}"/> instance for Accounts.</param>
        public AccountsController(IRepository<Account> accountRepository)
        {
            _accountRepository = accountRepository;
        }
        
        /// <summary>
        /// Gets all the Accounts from the system.
        /// </summary>
        /// <returns>A collection of all the Accounts added to the system.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Account>> GetAll()
        {
            var accounts = _accountRepository.GetAll().AsEnumerable();
            return Ok(accounts);
        }

        /// <summary>
        /// Gets a single Account with the specified Id.
        /// </summary>
        /// <param name="id">The Id of an Account to retrieve.</param>
        /// <returns>An Account with the specified Id.</returns>
        [HttpGet("{id}")]
        public ActionResult<string> Get(Guid id)
        {
            var account = _accountRepository.GetById(id);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        /// <summary>
        /// Inserts an Account into the system with the specified Id.
        /// </summary>
        /// <param name="id">The Id of the Account to add.</param>
        /// <param name="account">The Account to add.</param>
        /// <returns>An Http Ok if the insert was successful, BadRequest otherwise.</returns>
        [HttpPut("{id}")]
        public ActionResult Put(Guid id, [FromBody] Account account)
        {
            if (id != account.Id)
            {
                return BadRequest("Id does not match the Account passed in.");
            }

            if (_accountRepository.Insert(account))
            {
                return Ok();
            }

            return BadRequest();
        }

        /// <summary>
        /// Deletes an Account from the system.
        /// </summary>
        /// <param name="id">The Id of the Account to delete.</param>
        /// <returns>An Http Ok if the delete was successful, BadRequest otherwise.</returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            if (_accountRepository.Delete(id))
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
