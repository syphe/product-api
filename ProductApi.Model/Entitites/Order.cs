using System;
using ProductApi.DataAccess;

namespace ProductApi.Model.Entities
{
    /// <summary>
    /// An Order entity represents an Order of a particular Product for a particular Account.
    /// </summary>
    public class Order : IEntity
    {
        /// <inheritdoc/>
        public Guid Id { get; set; }

        /// <summary>Gets or sets the AccountId this Order belongs to.</summary>
        public Guid AccountId { get; set; }

        /// <summary>Gets or sets the ProductId of the Product this Order is for.</summary>
        public Guid ProductId { get; set; }

        /// <summary>Gets or sets a value indicating the number of units ordered.</summary>
        public long Quantity { get; set; }

        /// <summary>Gets or sets a value indicating the Price per unit of the product ordered.</summary>
        public decimal UnitPrice { get; set; }

        /// <summary>Gets or sets the Address of the Customer whom this Order is for.</summary>
        public string DeliveryAddress { get; set; }
    }
}
