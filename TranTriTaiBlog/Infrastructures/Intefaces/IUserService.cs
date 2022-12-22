using System;
using System.Threading.Tasks;
using DataModel.Infrastructure.Database.Migrations;
using DataModel.Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using TranTriTaiBlog.DTOs.Requests;
using TranTriTaiBlog.DTOs.Responses;

namespace TranTriTaiBlog.Infrastructures.Intefaces.UserServices
{
    public interface IUserService
    {
        /// <summary>
        /// Get User
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <returns>CommonResponse with User info</returns>
        Task<CommonResponse<UserDetailResponse>> GetUser(Guid userId);

        /// <summary>
        /// List User Skills
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <returns>CommonResponse with list User Skill info</returns>
        Task<CommonResponse<UserSkillsResponse[]>> GetListUserSkills(Guid userId);
        
        /// <summary>
        /// Add new skills for user
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <param name="request">body request</param>
        /// <returns>CommonResponse with list new User Skill info</returns>
        Task<CommonResponse<string>> CreateUserSkills(Guid userId, CreateUserSkillRequest request);

        /// <summary>
        /// Delete a skill of a user
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <param name="skillId">skill Id</param>
        /// <returns>CommonResponse delete comfirmation</returns>
        Task<CommonResponse<string>> DeleteUserSkill(Guid userId, Guid skillId);
        
        /// <summary>
        /// Update user detail information
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <param name="request">body request</param>
        /// <returns>CommonResponse with update confirmation</returns>
        Task<CommonResponse<UpdateUserDetailResponse>> UpdateUserDetail(Guid userId, UpdateUserDetailRequest request);

        /// <summary>
        /// Register new User
        /// </summary>
        /// <param name="request">body request</param>
        /// <return>CommonResponse with new user</return>
        Task<CommonResponse<UserResponse>> RegistrationNewUser(UserRegistrationRequest request);
        
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="request">body request</param>
        /// <return>CommonResponse with JWT token</return>
        Task<CommonResponse<UserLoginResponse>> Login(UserLoginRequest request);

        /// <summary>
        /// Logout
        /// </summary>
        /// <param name="tokenId">login token id</param>
        /// <return>CommonResponse with delete successful confirmation</return>
        Task<CommonResponse<string>> Logout(Guid tokenId);

        string Hash(string password);

        bool IsMatch(string hashedPassword, string plainTextPassword);

        string JWTToken(User user, string secret, double expiry);

        Guid ExtractUserIdFromToken(HttpRequest request);
    }
}

