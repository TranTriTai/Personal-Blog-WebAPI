using System;
using System.Threading.Tasks;
using DataModel.Infrastructure.Models;

namespace DataModel.Infrastructure.Interfaces.Query
{
    public interface ISkillQuery : IQuery<Skill>
    {
        /// <summary>
        /// Get a skill list
        /// </summary>
        /// <returns>a skill list</returns>
        Task<Skill[]> GetListSkills();
    }
}

