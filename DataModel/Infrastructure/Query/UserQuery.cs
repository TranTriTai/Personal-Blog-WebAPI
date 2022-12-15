using System;
using System.Linq;
using System.Threading.Tasks;
using DataModel.Infrastructure.Database;
using DataModel.Infrastructure.Interfaces.Query;
using DataModel.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace DataModel.Infrastructure.Query
{
    public class UserQuery : BaseQuery<User>, IUserQuery
    {
        public UserQuery() { }

        public UserQuery(BlogDbContext dbContext) : base(dbContext) { }

        public async Task<UserSkill[]> GetUserSkillsById(Guid userId)
        {
            if(userId == Guid.Empty)
            {
                throw new Exception("Invalid Input");
            }

            try
            {
                var query = await _dbContext.UserSkills
                            .Where(x => x.UserId == userId)
                            .ToArrayAsync();

                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

