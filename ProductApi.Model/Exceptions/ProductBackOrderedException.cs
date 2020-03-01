using System;

namespace ProductApi.Model.Exceptions
{
    public class ProductBackOrderedException : Exception
    {
        public ProductBackOrderedException() : base("The product ordered is on BackOrder and no more may be ordered.")
        {
        }
    }
}
