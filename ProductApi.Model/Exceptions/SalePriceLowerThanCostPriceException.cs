using System;

namespace ProductApi.Model.Exceptions
{
    public class SalePriceLowerThanCostPriceException : Exception
    {
        public SalePriceLowerThanCostPriceException(decimal salePrice, decimal costPrice)
            : base($"The sale price {salePrice} Euro must not be less than the cost price {costPrice} Euro for the product ordered")
        {
        }
    }
}
