using System;
using DataModel.Infrastructure.Models;
using System.Threading.Tasks;
using DataModel.Infrastructure.Implementations;

namespace DataModel.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Init new IRepository or get an existing one
        /// </summary>
        /// <typeparam name="T">IEntity</typeparam>
        /// <returns>IRepository</returns>
        IRepository<T> GetRepository<T>() where T : DbEntity;

        /// <summary>
        /// Apply all changes performed on this context to the database
        /// </summary>
        /// <returns>Number of state entities written to the database</returns>
        Task<int> CompleteAsync();
    }
}

