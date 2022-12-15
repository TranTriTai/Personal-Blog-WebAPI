using System;
using System.Threading.Tasks;

namespace DataModel.Infrastructure.Interfaces.Query
{
    public interface IQuery<T> where T : class
    {
        /// <summary>
        /// Get entity by its id.
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>Entity</returns>
        Task<T> GetById(Guid id);
    }
}

