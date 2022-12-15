using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModel.Infrastructure.Database;
using DataModel.Infrastructure.Interfaces;
using DataModel.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace DataModel.Infrastructure.Implementations
{
    public class BaseRepository<T> : IRepository<T> where T : DbEntity
    {
        private readonly BlogDbContext _dbContext;

        public BaseRepository(BlogDbContext blogDbContext)
        {
            _dbContext = blogDbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            return (await _dbContext.AddAsync(entity)).Entity;
        }

        public Task AddRangeAsync(IEnumerable<T> entities)
        {
            return _dbContext.AddRangeAsync(entities);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdsAsync(Guid id, Guid otherId)
        {
            return await _dbContext.Set<T>().FindAsync(id, otherId);
        }

        public IQueryable<T> GetQueryable()
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        public void HardDelete(T entity)
        {
            _dbContext.Remove(entity);
        }

        public void HardDeleteRange(IEnumerable<T> entities)
        {
            _dbContext.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            if (typeof(T) == typeof(BaseEntity))
            {
                (entity as BaseEntity).UpdatedAt = DateTime.UtcNow;
            }

            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}

