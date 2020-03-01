using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductApi.Controllers;
using ProductApi.DataAccess;
using ProductApi.Model.Entities;
using Xunit;

namespace ProductApi.Tests.Controller
{
    public class AccountsControllerTests
    {
        [Fact]
        public void Test_GetAll_ReturnsAllAccountsFromRepository()
        {
            var testAccounts = new List<Account>()
            {
                new Account { Id = Guid.NewGuid() },
                new Account { Id = Guid.NewGuid() }
            };

            var accountRepository = new Mock<IRepository<Account>>();
            accountRepository.Setup(x => x.GetAll()).Returns(testAccounts.AsQueryable());

            var accountsController = new AccountsController(accountRepository.Object);

            var result = accountsController.GetAll();

            Assert.IsType<OkObjectResult>(result.Result);

            var okObjectResult = result.Result as OkObjectResult;
            var accountsResult = okObjectResult.Value as IEnumerable<Account>;
            Assert.NotNull(accountsResult);
            Assert.Equal(accountsResult.Count(), testAccounts.Count);
        }
    }
}
