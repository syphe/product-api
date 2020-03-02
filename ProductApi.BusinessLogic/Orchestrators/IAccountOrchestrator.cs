using System;
using System.Collections.Generic;
using System.Text;
using ProductApi.Model.Entities;

namespace ProductApi.BusinessLogic.Orchestrators
{
    public interface IAccountOrchestrator
    {
        IEnumerable<Account> GetAll();

        Account GetById(Guid id);

        void CreateAccount(Account account);

        void DeleteAccount(Guid id);
    }
}
