using System;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TranTriTaiBlog.DTOs.Requests;
using TranTriTaiBlog.DTOs.Responses;
using TranTriTaiBlog.Filter;
using TranTriTaiBlog.Infrastructures.Intefaces;

namespace TranTriTaiBlog.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _skillService;

        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        /// Post api/skills
        /// <summary>
        /// create a Skill
        /// </summary>
        /// <param name="request">Attributes to be updated</param>
        /// <returns>CommonResponse with Status Code corresponding to success of the create</returns>
        [SwaggerOperation(Summary = "Create a new skill")]
        [HttpPost(Name = "CreateSkill")]
        [ApiAuthentication()]
        [ProducesResponseType(typeof(CommonResponse<CreateSkillResponse>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateSkill(CreateSkillRequest request)
        {
            var result = await _skillService.CreateSkill(request);
            return StatusCode(result.StatusCode, result);
        }

        /// Get api/skills
        /// <summary>
        /// get list of skills
        /// </summary>
        /// <returns>CommonResponse with list of skills</returns>
        [SwaggerOperation(Summary = "Get list of skills")]
        [HttpGet(Name = "GetListSkills")]
        [ProducesResponseType(typeof(CommonResponse<SkillsResponse[]>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetListSkills()
        {
            var result = await _skillService.GetListSkills();

            return StatusCode(result.StatusCode, result);
        }

        /// PUT api/skills/{skillId}
        /// <summary>
        /// update Skill
        /// </summary>
        /// <param name="skillId">Id of the Skill</param>
        /// <param name="request">Attributes to be updated</param>
        /// <returns>CommonResponse with Status Code corresponding to success of the update </returns>
        [SwaggerOperation(Summary = "Update a skill")]
        [HttpPut("{skillId}", Name = "UpdateSkill")]
        [ApiAuthentication()]
        [ProducesResponseType(typeof(CommonResponse<UpdateSkillResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateSkill([FromRoute] Guid skillId, [FromBody] UpdateSkillRequest request)
        {
            var response = await _skillService.UpdateSkill(skillId, request);
            return StatusCode(response.StatusCode, response);
        }

        /// DELETE api/skills/{skillId}
        /// <summary>
        /// delete a skill
        /// </summary>
        /// <param name="skillId">Id of the skill</param>
        /// <return>Response with confirmation that the skill has been deleted</return>
        [SwaggerOperation(Summary = "Delete a skill")]
        [HttpDelete("{skillId}", Name = "DeleteSkill")]
        [ApiAuthentication()]
        [ProducesResponseType(typeof(CommonResponse<string>), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteSkill([FromRoute] Guid skillId)
        {
            var response = await _skillService.DeleteSkill(skillId);
            if (response.StatusCode == StatusCodes.Status204NoContent)
                return NoContent();
            return StatusCode(response.StatusCode, response);
        }
    }
}

