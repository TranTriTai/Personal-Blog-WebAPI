using System;
using System.Threading.Tasks;
using DataModel.Infrastructure.Database;
using DataModel.Infrastructure.Interfaces.Query;

namespace DataModel.Infrastructure.Query
{
    public class BaseQuery<T> : IQuery<T> where T : class
    {
        protected BlogDbContext _dbContext;

        public BaseQuery() { }

        public BaseQuery(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<T> GetById(Guid id)
        {
                return await _dbContext.Set<T>().FindAsync(id);
        }
    }
}

