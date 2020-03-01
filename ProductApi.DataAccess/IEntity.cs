using System;

namespace ProductApi.DataAccess
{
    /// <summary>
    /// The base interface for any entity to be used by the repository.
    /// </summary>
    public interface IEntity
    {
        /// <summary>Gets the Id of this entity.</summary>
        Guid Id { get; }
    }
}
