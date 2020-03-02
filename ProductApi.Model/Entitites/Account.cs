using System;
using ProductApi.DataAccess;

namespace ProductApi.Model.Entities
{
    /// <summary>
    /// A customer's Account entity.
    /// </summary>
    public class Account : IEntity
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Account"/>
        /// </summary>
        /// <param name="name">The Name of this Account.</param>
        public Account(string name, Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
            Name = name;
        }

        /// <summary>Gets or sets the Id of this <see cref="Account"/></summary>
        public Guid Id { get; private set; }

        /// <summary>Gets or sets the Name of this Account.</summary>
        public string Name { get; set; }
    }
}