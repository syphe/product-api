using System;
using ProductApi.DataAccess;

namespace ProductApi.Model.Entities
{
    /// <summary>
    /// The Product entity represents a Product within the system which may be ordered by a Customer.
    /// </summary>
    public class Product : IEntity
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Product"/>
        /// </summary>
        /// <param name="accountId">The AccountId for the Account this Product applies to.</param>
        /// <param name="name">The Name of this Product.</param>
        /// <param name="costPrice">A value indicating the Cost Price to the Business for this Product.</param>
        public Product(Guid accountId, string name, decimal costPrice)
        {
            AccountId = accountId;
            Name = name;
            CostPrice = costPrice;
        }

        /// <inheritdoc/>
        public Guid Id { get; private set; }

        /// <summary>Gets or sets the AccountId for the Account this Product applies to.</summary>
        public Guid AccountId { get; set; }

        /// <summary>Gets or sets the Name of this Product.</summary>
        public string Name { get; set; }

        /// <summary>Gets or sets a value indicating the Cost Price to the Business for this Product.</summary>
        public decimal CostPrice { get; set; }
    }
}
