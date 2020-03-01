using System;

namespace ProductApi.DataAccess.Entities
{
    /// <summary>
    /// A customer's Account entity.
    /// </summary>
    public class Account : IEntity
    {
        /// <summary>Gets or sets the Id of this <see cref="Account"/></summary>
        public Guid Id { get; set; }
    }
}