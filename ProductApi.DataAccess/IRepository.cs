using System;
using System.Linq;

namespace ProductApi.DataAccess
{
    /// <summary>
    /// The interface for implementation of the repository pattern.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class, IEntity
    {
        /// <summary>
        /// Inserts a record into the repository.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        /// <returns>
        /// A value indicating whether the insert was successful or not,
        /// it may fail if a record already exists with the entities Id.</returns>
        bool Insert(T entity);

        /// <summary>
        /// Deletes a record from the repository.
        /// </summary>
        /// <param name="id">The Id of the entity.</param>
        /// <returns>
        /// A value indicating whether the delete was successful or not,
        /// it may fail if no record exists with the Id specified.
        /// </returns>
        bool Delete(Guid id);
        
        /// <summary>
        /// Gets an entity from the repository for the specified Id.
        /// </summary>
        /// <param name="id">The Id of the entity to retrieve.</param>
        /// <returns>A non-null instance of the entity if it exists, otherwise null.</returns>
        T GetById(Guid id);

        /// <summary>
        /// Gets a queryable collection of all the entities in the repository.
        /// </summary>
        /// <returns>A queryable collection of all the entities in the repository.</returns>
        IQueryable<T> GetAll();
    }
}