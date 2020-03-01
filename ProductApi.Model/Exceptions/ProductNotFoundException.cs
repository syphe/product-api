using System;
using System.Collections.Generic;
using System.Text;

namespace ProductApi.Model.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(Guid id) : base($"The Product with the Id {id} couldn't be found.")
        {
        }
    }
}
