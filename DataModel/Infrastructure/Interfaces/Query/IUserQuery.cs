using System;
using System.Threading.Tasks;
using DataModel.Infrastructure.Models;

namespace DataModel.Infrastructure.Interfaces.Query
{
    public interface IUserQuery : IQuery<User>
    {
        /// <summary>
        /// Get user's skills by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>user's skills</returns>
        Task<UserSkill[]> GetUserSkillsById(Guid userId);
    }
}

