using System;
using System.Collections.Generic;
using System.Text;
using ProductApi.DataAccess;
using ProductApi.Model.Entities;

namespace ProductApi.BusinessLogic.Orchestrators
{
    internal class AccountOrchestrator : IAccountOrchestrator
    {
        private readonly IRepository<Account> _accountRepository;

        public AccountOrchestrator(IRepository<Account> accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public void CreateAccount(Account account)
        {
            if (!_accountRepository.Insert(account))
            {
                throw new Exception();
            }
        }

        public void DeleteAccount(Guid id)
        {
            if (!_accountRepository.Delete(id))
            {
                throw new ArgumentException(nameof(id));
            }
        }

        public IEnumerable<Account> GetAll()
        {
            var accounts = _accountRepository.GetAll();
            return accounts;
        }

        public Account GetById(Guid id)
        {
            var account = _accountRepository.GetById(id);
            return account;
        }
    }
}
