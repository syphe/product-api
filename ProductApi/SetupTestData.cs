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
        private readonly IRepository<Product> _productRepository;

        /// <summary>
        /// Initializes the SetupTestData class.
        /// </summary>
        /// <param name="accountRepository">The <see cref="IRepository{T}"/> repository for the Account entity.</param>
        public SetupTestData(
            IRepository<Account> accountRepository,
            IRepository<Product> productRepository)
        {
            _accountRepository = accountRepository;
            _productRepository = productRepository;
        }

        /// <summary>
        /// Run the test data creation.
        /// </summary>
        public void Run()
        {
            var acmeLtdAccount = new Account("Acme Ltd", new Guid("c6c8c4a8-7924-4836-8a79-e25ab1466bad"));
            _accountRepository.Insert(acmeLtdAccount);

            var matchstickCompanyAccount = new Account("Matchstick Company", new Guid("71328bc4-6fd0-4b8f-9a03-5cb076107236"));
            _accountRepository.Insert(matchstickCompanyAccount);

            _productRepository.Insert(new Product(acmeLtdAccount.Id, "Pen", 2.0M, new Guid("6ee608f0-7323-477d-bed5-94ee31df2736")));
            _productRepository.Insert(new Product(acmeLtdAccount.Id, "Battery", 3.0M, new Guid("dbea726e-4b3f-4abc-8fed-c94d43710c68")));
            _productRepository.Insert(new Product(acmeLtdAccount.Id, "Paper", 15.0M, new Guid("0c543a2e-0b2b-4e2b-b180-6eeaec476801")));

            _productRepository.Insert(new Product(matchstickCompanyAccount.Id, "Keyboard", 15.0M, new Guid("ad0a2246-9478-4394-8b12-7a281439ea8e")));
            _productRepository.Insert(new Product(matchstickCompanyAccount.Id, "Speaker", 50.0M, new Guid("81945f33-66ad-4559-9f4d-facf7c7bd743")));
            _productRepository.Insert(new Product(matchstickCompanyAccount.Id, "Monitor", 300.0M, new Guid("9a2df2c3-551d-4ef4-8abe-16bcf5c3582f")));
        }
    }
}