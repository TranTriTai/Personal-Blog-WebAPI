using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataModel.Infrastructure.Interfaces
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Add an entity
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Entity after added</returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Add a list of entity
        /// </summary>
        /// <param name="entities">Entities</param>
        Task AddRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// Hard delete an entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void HardDelete(T entity);

        /// <summary>
        /// Hard delete a list of entity
        /// </summary>
        /// <param name="entities">Entities</param>
        void HardDeleteRange(IEnumerable<T> entities);

        /// <summary>
        /// Change state of entity to modified
        /// </summary>
        /// <param name="entity">Entity</param>
        void Update(T entity);

        /// <summary>
        /// Get entity by its id.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>Entity</returns>
        Task<T> GetByIdAsync(Guid id);

        /// <summary>
        /// Get entity by ids.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>Entity</returns>
        Task<T> GetByIdsAsync(Guid id, Guid otherId);

        /// <summary>
        /// Get IQueryable to perform specific query
        /// </summary>
        /// <returns>IQueryable</returns>
        IQueryable<T> GetQueryable();
    }
}

