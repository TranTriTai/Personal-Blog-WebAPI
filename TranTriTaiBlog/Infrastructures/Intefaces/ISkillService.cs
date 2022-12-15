using System;
using TranTriTaiBlog.DTOs.Requests;
using TranTriTaiBlog.DTOs.Responses;

namespace TranTriTaiBlog.Infrastructures.Intefaces
{
    public interface ISkillService
    {
        /// <summary>
        /// Create Skill.
        /// </summary>
        /// <param name="request">contains skill info</param>
        /// <returns>CommonResponse with skill info</returns>
        Task<CommonResponse<CreateSkillResponse>> CreateSkill(CreateSkillRequest request);

        /// <summary>
        /// List Skills
        /// </summary>
        /// <returns>CommonResponse with list Skill info</returns>
        Task<CommonResponse<SkillsResponse[]>> GetListSkills();

        /// <summary>
        /// Update a Skill
        /// </summary>
        /// <param name="skillId"></param>
        /// <param name="request"></param>
        /// <returns>CommonResponse with successful message</returns>
        Task<CommonResponse<UpdateSkillResponse>> UpdateSkill(Guid skillId, UpdateSkillRequest request);

        /// <summary>
        /// Delete a Skill
        /// </summary>
        /// <param name="skillId"></param>
        /// <returns>CommonResponse with no content message</returns>
        Task<CommonResponse<string>> DeleteSkill(Guid skillId);
    }
}

