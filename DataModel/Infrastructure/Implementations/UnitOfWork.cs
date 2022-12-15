using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataModel.Infrastructure.Database;
using DataModel.Infrastructure.Interfaces;
using DataModel.Infrastructure.Models;

namespace DataModel.Infrastructure.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlogDbContext _dbContext;

        private Dictionary<string, object> _repositories;

        public UnitOfWork(BlogDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Task<int> CompleteAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public IRepository<T> GetRepository<T>() where T : DbEntity
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, object>();
            }

            string type = typeof(T).Name;

            if (_repositories.ContainsKey(type) == false)
            {
                BaseRepository<T> repositoryInstance = new BaseRepository<T>(_dbContext);
                _repositories.Add(type, repositoryInstance);
            }

            return (IRepository<T>)(BaseRepository<T>)_repositories[type];
        }
    }
}

