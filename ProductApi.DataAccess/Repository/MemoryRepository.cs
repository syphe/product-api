using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductApi.DataAccess.Repository
{
    /// <summary>
    /// An impelementaion of the <see cref="IRepository{T}"/> interface.
    /// This implementation is just a simple Dictionary, for demonstration purposes, 
    /// to satisfy the requirement: "Please only invest minimal effort in data persistence"
    /// </summary>
    /// <typeparam name="T">The type of the entity for this repository.</typeparam>
    internal class MemoryRepository<T> : IRepository<T> where T : class, IEntity
    {
        private IDictionary<Guid, T> _records = new Dictionary<Guid, T>();

        /// <inheritdoc/>
        public IQueryable<T> GetAll()
        {
            return _records.Select(x => x.Value).AsQueryable();
        }

        /// <inheritdoc/>
        public T GetById(Guid id)
        {
            if (_records.ContainsKey(id))
            {
                return _records[id];
            }

            return null;
        }

        /// <inheritdoc/>
        public bool Insert(T entity)
        {
            if (_records.ContainsKey(entity.Id))
            {
                return false;
            }

            _records[entity.Id] = entity;
            return true;
        }

        /// <inheritdoc/>
        public bool Delete(Guid id)
        {
            if (!_records.ContainsKey(id))
            {
                return false;
            }

            _records.Remove(id);
            return true;
        }
    }
}