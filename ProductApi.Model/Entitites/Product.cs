using System;
using ProductApi.DataAccess;

namespace ProductApi.Model.Entities
{
    /// <summary>
    /// The Product entity represents a Product within the system which may be ordered by a Customer.
    /// </summary>
    public class Product : IEntity
    {
        /// <inheritdoc/>
        public Guid Id { get; set; }

        /// <summary>Gets or sets the AccountId for the Account this Product applies to.</summary>
        public Guid AccountId { get; set; }

        /// <summary>Gets or sets the Name of this Product.</summary>
        public string Name { get; set; }

        /// <summary>Gets or sets a value indicating the Cost Price to the Business for this Product.</summary>
        public decimal CostPrice { get; set; }
    }
}
