using System;
using System.Threading.Tasks;
using DataModel.Infrastructure.Database;
using DataModel.Infrastructure.Interfaces.Query;
using DataModel.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace DataModel.Infrastructure.Query
{
    public class SkillQuery : BaseQuery<Skill>, ISkillQuery
    {
        public SkillQuery() { }

        public SkillQuery(BlogDbContext dbContext) : base(dbContext) { }

        public async Task<Skill[]> GetListSkills()
        {
            var query = await _dbContext.Skills.ToArrayAsync();

            return query;
        }
    }
}

