using System;
using ProductApi.DataAccess;

namespace ProductApi.Model.Entities
{
    /// <summary>
    /// An Order entity represents an Order of a particular Product for a particular Account.
    /// </summary>
    public class Order : IEntity
    {
        /// <summary>
        /// Initializes a new instance of an <see cref="Order"/>
        /// </summary>
        /// <param name="accountId">The AccountId this Order belongs to.</param>
        /// <param name="productId">The ProductId of the Product this Order is for.</param>
        /// <param name="quantity">A value indicating the number of units ordered.</param>
        /// <param name="unitPrice">A value indicating the Price per unit of the product ordered.</param>
        /// <param name="deliveryAddress">The Address of the Customer whom this Order is for.</param>
        public Order(Guid accountId, Guid productId, long quantity, decimal unitPrice, string deliveryAddress)
        {
            Id = Guid.NewGuid();
            AccountId = accountId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            DeliveryAddress = deliveryAddress;
        }

        /// <inheritdoc/>
        public Guid Id { get; private set; }

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

        /// <summary>Gets or sets a value indicating whether the Customer has paid for this order or not.</summary>
        public bool Paid { get; set; }
    }
}
