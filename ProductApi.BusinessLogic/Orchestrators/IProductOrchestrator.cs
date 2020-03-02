using System;
using System.Collections.Generic;
using System.Text;
using ProductApi.Model.Entities;

namespace ProductApi.BusinessLogic.Orchestrators
{
    public interface IProductOrchestrator
    {
        IEnumerable<Product> GetAll(Guid accountId);
    }
}
