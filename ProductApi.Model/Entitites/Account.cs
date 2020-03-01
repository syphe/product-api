using System;
using ProductApi.DataAccess;

namespace ProductApi.Model.Entities
{
    /// <summary>
    /// A customer's Account entity.
    /// </summary>
    public class Account : IEntity
    {
        /// <summary>Gets or sets the Id of this <see cref="Account"/></summary>
        public Guid Id { get; set; }

        /// <summary>Gets or sets the Name of this Account.</summary>
        public string Name { get; set; }
    }
}