using System;
using System.Threading.Tasks;
using AutoMapper;
using DataModel.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TranTriTaiBlog.DTOs.Requests;
using TranTriTaiBlog.DTOs.Responses;
using TranTriTaiBlog.Filter;
using TranTriTaiBlog.Infrastructures.Intefaces.UserServices;

namespace TranTriTaiBlog.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get User.
        /// </summary>
        /// <returns>User info</returns>
        [SwaggerOperation(Summary = "Get User Information")]
        [HttpGet("{userId}", Name = "UserDetail")]
        [ProducesResponseType(typeof(CommonResponse<UserDetailResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserDetail([FromRoute] Guid userId)
        {
            var result = await _userService.GetUser(userId);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Update User Detail.
        /// </summary>
        /// <param name="userId">Id of an user</param>
        /// <param name="request">request body</param>
        /// <returns>CommonResponse with Status Code corresponding to success of the update</returns>
        [SwaggerOperation(Summary = "Update User Information")]
        [HttpPut("{userId}/userDetail", Name = "Update user detail")]
        [ApiAuthentication()]
        [ProducesResponseType(typeof(CommonResponse<UpdateUserDetailResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUserDetail([FromRoute] Guid userId, [FromBody] UpdateUserDetailRequest request)
        {
            var response = await _userService.UpdateUserDetail(userId, request);
            return StatusCode(response.StatusCode, response);
        }


        /// <summary>
        /// Get a List User Skills By UserId
        /// </summary>
        /// <returns>List User Skills</returns>
        [SwaggerOperation(Summary = "Get User Skills")]
        [HttpGet("{userId}/userSkills")]
        [ProducesResponseType(typeof(CommonResponse<UserSkillsResponse[]>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserSkills([FromRoute] Guid userId)
        {
            var result = await _userService.GetListUserSkills(userId);
            if (result.StatusCode == StatusCodes.Status204NoContent)
            {
                return NoContent();
            }
            return StatusCode(result.StatusCode, result);
        }

        /// <summary
        /// Add new skills for a user
        /// </summary>
        /// <return> a new skills for a user</return>
        [SwaggerOperation(Summary = "Create a new User Skill")]
        [HttpPost("{userId}/userSkills", Name = "AddCourseForUser")]
        [ApiAuthentication()]
        [ProducesResponseType(typeof(CommonResponse<string>), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateUserSkills([FromRoute] Guid userId, [FromBody] CreateUserSkillRequest request)
        {
            var result = await _userService.CreateUserSkills(userId, request);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Delete a skill from a user
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <param name="skillId">Id of skill</param>
        /// <return>Response with confirmation that the skill has been deleted</return>
        [SwaggerOperation(Summary = "Delete a skill of an user")]
        [HttpDelete("{userId}/userSkills/{skillId}", Name = "DeleteUserSkill")]
        [ApiAuthentication()]
        [ProducesResponseType(typeof(CommonResponse<string>), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteUserSkill([FromRoute] Guid userId, [FromRoute] Guid skillId)
        {
            var response = await _userService.DeleteUserSkill(userId, skillId);
            if (response.StatusCode == StatusCodes.Status204NoContent)
                return NoContent();
            return StatusCode(response.StatusCode, response);
        }

        ///<summary>
        /// Register a new User
        /// </summary>
        /// <param name="request"> body request to create new user</param>
        /// <return>Response with new user Info</return>
        [SwaggerOperation(Summary = "Register a new User")]
        [HttpPost("registration", Name = "User Registration")]
        [ProducesResponseType(typeof(CommonResponse<UserResponse>), StatusCodes.Status201Created)]
        public async Task<IActionResult> RegistrationNewUser(UserRegistrationRequest request)
        {
            var result = await _userService.RegistrationNewUser(request);
            return StatusCode(result.StatusCode, result);
        }

        ///<summary>
        /// Login with Email and Password
        /// </summary>
        /// <param name="request">Email and Password</param>
        /// <return>JWT Token</return>
        [SwaggerOperation(Summary = "Login with Email and Password")]
        [HttpPost("token")]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            var result = await _userService.Login(request);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// delete a token
        /// </summary>
        /// <param name="tokenId">Id of the token</param>
        /// <return>Response with confirmation that the token has been deleted</return>
        [SwaggerOperation(Summary = "Logout")]
        [HttpDelete("{tokenId}", Name ="Logout")]
        [ApiAuthentication()]
        [ProducesResponseType(typeof(CommonResponse<string>), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Logout([FromRoute] Guid tokenId)
        {
            var response = await _userService.Logout(tokenId);
            if (response.StatusCode == StatusCodes.Status204NoContent)
                return NoContent();
            return StatusCode(response.StatusCode, response);
        }
    }
}


