using System;
using System.Collections.Generic;
using System.Text;

namespace ProductApi.Model.Exceptions
{
    public class CustomerHasOutstandingOrdersException : Exception
    {
        public CustomerHasOutstandingOrdersException(decimal valueLimit)
            : base($"The customer has outstanding orders exceeding {valueLimit} Euro, no more orders are able to be made")
        {
        }
    }
}
