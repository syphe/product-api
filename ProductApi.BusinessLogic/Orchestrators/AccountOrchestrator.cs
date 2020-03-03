using System;
using System.Collections.Generic;
using System.Text;
using ProductApi.DataAccess;
using ProductApi.Model.Entities;

namespace ProductApi.BusinessLogic.Orchestrators
{
    /// <summary>
    /// Implementation of the <see cref="IAccountOrchestrator"/> interface.
    /// </summary>
    internal class AccountOrchestrator : IAccountOrchestrator
    {
        private readonly IRepository<Account> _accountRepository;

        /// <summary>
        /// Initializes a new instance of the AccountOrchestrator class.
        /// </summary>
        /// <param name="accountRepository">The <see cref="IRepository{T}"/> required for Accounts.</param>
        public AccountOrchestrator(IRepository<Account> accountRepository)
        {
            _accountRepository = accountRepository;
        }

        /// <inheritdoc/>
        public void CreateAccount(Account account)
        {
            if (!_accountRepository.Insert(account))
            {
                throw new Exception();
            }
        }

        /// <inheritdoc/>
        public void DeleteAccount(Guid id)
        {
            if (!_accountRepository.Delete(id))
            {
                throw new ArgumentException(nameof(id));
            }
        }

        /// <inheritdoc/>
        public IEnumerable<Account> GetAll()
        {
            var accounts = _accountRepository.GetAll();
            return accounts;
        }

        /// <inheritdoc/>
        public Account GetById(Guid id)
        {
            var account = _accountRepository.GetById(id);
            return account;
        }
    }
}
