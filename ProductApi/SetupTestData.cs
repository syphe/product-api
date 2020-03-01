using System;
using ProductApi.DataAccess;
using ProductApi.Model.Entities;

namespace ProductApi
{
    /// <summary>
    /// This class sets up the in memory repositories with the test data required
    /// to show a functional demo.
    /// </summary>
    internal class SetupTestData
    {
        private readonly IRepository<Account> _accountRepository;

        /// <summary>
        /// Initializes the SetupTestData class.
        /// </summary>
        /// <param name="accountRepository">The <see cref="IRepository{T}"/> repository for the Account entity.</param>
        public SetupTestData(IRepository<Account> accountRepository)
        {
            _accountRepository = accountRepository;
        }

        /// <summary>
        /// Run the test data creation.
        /// </summary>
        public void Run()
        {
            _accountRepository.Insert(new Account
            {
                Id = Guid.NewGuid(),
            });
            _accountRepository.Insert(new Account
            {
                Id = Guid.NewGuid(),
            });
        }
    }
}