using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using DataModel.Enums;
using DataModel.Infrastructure.Interfaces;
using DataModel.Infrastructure.Interfaces.Query;
using DataModel.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TranTriTaiBlog.DTOs.Requests;
using TranTriTaiBlog.DTOs.Responses;
using TranTriTaiBlog.Infrastructures.Constants;
using TranTriTaiBlog.Infrastructures.Helper.Constants;
using TranTriTaiBlog.Infrastructures.Helper.MessageUtil;
using TranTriTaiBlog.Infrastructures.Intefaces.UserServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TranTriTaiBlog.Infrastructures.Services.UserServices
{
    public partial class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserQuery _query;
        private readonly IConfiguration _configuration;

        public UserService(IMapper mapper, ILogger<UserService> logger,
            IUnitOfWork unitOfWork, IUserQuery query, IConfiguration configuration)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _query = query;
            _configuration = configuration;
        }

        public async Task<CommonResponse<string>> CreateUserSkills(Guid userId, CreateUserSkillRequest request)
        {
            if (request == null || userId == Guid.Empty || Enum.IsDefined(typeof(SkillLevel), request?.Level) == false)
            {
                return new CommonResponse<string>(StatusCodes.Status400BadRequest,
                    ErrorMsgUtil.GetBadRequestMsg(nameof(UserSkill)), null);
            }

            try
            {
                var user = await _query.GetById(userId);

                if (user == null)
                {
                    return new CommonResponse<string>(StatusCodes.Status422UnprocessableEntity,
                        ErrorMsgUtil.GetUnprocessibleMsg(nameof(User)), null);
                }

                UserSkill userSkillEntity = _mapper.Map<UserSkill>(request);
                userSkillEntity.UserId = userId;
                userSkillEntity.CreatedById = userId;

                IRepository<UserSkill> repo = _unitOfWork.GetRepository<UserSkill>();

                await repo.AddAsync(userSkillEntity);
                await _unitOfWork.CompleteAsync();

                return new CommonResponse<string>(StatusCodes.Status201Created, "User Skill has created");
            }
            
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new Exception(ErrorMsgUtil.GetErrWhenAdding(nameof(UserSkill)));
            }
        }

        public async Task<CommonResponse<string>> DeleteUserSkill(Guid userId, Guid skillId)
        {
            if (userId == Guid.Empty || skillId == Guid.Empty)
            {
                return new CommonResponse<string>(StatusCodes.Status400BadRequest,
                    ErrorMsgUtil.GetBadRequestMsg(nameof(UserSkill)), "one of the Id is empty");
            }

            try
            {
                IRepository<UserSkill> repo = _unitOfWork.GetRepository<UserSkill>();
                UserSkill userSkill = await repo.GetByIdsAsync(userId, skillId);
                if (userSkill == null)
                {
                    return new CommonResponse<string>(StatusCodes.Status404NotFound,
                        ErrorMsgUtil.GetCannotFindMsg(nameof(UserSkill)), null);
                }

                if (userSkill.UserId != userId)
                {
                    return new CommonResponse<string>(StatusCodes.Status422UnprocessableEntity,
                        ErrorMsgUtil.GetUnprocessibleMsg(nameof(UserSkill)), null);
                }

                repo.HardDelete(userSkill);
                await _unitOfWork.CompleteAsync();

                return new CommonResponse<string>(StatusCodes.Status204NoContent, "Delete a record Successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error at ${nameof(DeleteUserSkill)} :" + ex.Message);
                throw new Exception(ErrorMsgUtil.GetErrWhenGetting(nameof(UserSkill)));
            }
        }

        public async Task<CommonResponse<UserSkillsResponse[]>> GetListUserSkills(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return new CommonResponse<UserSkillsResponse[]>(StatusCodes.Status400BadRequest,
                    ErrorMsgUtil.GetBadRequestMsg(nameof(User)), null);
            }

            try
            {
                var result = await _query.GetUserSkillsById(userId);

                if (result == null)
                {
                    return new CommonResponse<UserSkillsResponse[]>(StatusCodes.Status404NotFound,
                        ErrorMsgUtil.GetCannotFindMsg(nameof(UserSkill)), null);
                }

                if ((result.Count() == 0))
                {
                    return new CommonResponse<UserSkillsResponse[]>(StatusCodes.Status204NoContent);
                }

                var response = _mapper.Map<UserSkillsResponse[]>(result);
                return new CommonResponse<UserSkillsResponse[]>(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new Exception(ErrorMsgUtil.GetErrWhenGetting(nameof(User)));
            }
        }

        public async Task<CommonResponse<UserDetailResponse>> GetUser(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return new CommonResponse<UserDetailResponse>(StatusCodes.Status400BadRequest,
                    ErrorMsgUtil.GetBadRequestMsg(nameof(userId)), null);
            }

            try
            {
                var user = await _query.GetById(userId);

                if ( user == null)
                {
                    return new CommonResponse<UserDetailResponse>(StatusCodes.Status404NotFound,
                        ErrorMsgUtil.GetCannotFindMsg(nameof(User)), null);
                }

                return new CommonResponse<UserDetailResponse>(StatusCodes.Status200OK,
                    _mapper.Map<UserDetailResponse>(user));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error at ${nameof(GetUser)} :" + ex.Message);
                throw new Exception(ErrorMsgUtil.GetErrWhenGetting(nameof(User)));
            }
        }

        public async Task<CommonResponse<UserResponse>> RegistrationNewUser(UserRegistrationRequest request)
        {
            if (request == null)
            {
                return new CommonResponse<UserResponse>(StatusCodes.Status400BadRequest,
                    ErrorMsgUtil.GetBadRequestMsg(nameof(User)), null);
            }

            try
            {
                IRepository<User> repo = _unitOfWork.GetRepository<User>();
                if (await IsEmailDuplicate(repo, request.Email))
                {
                    return new CommonResponse<UserResponse>(StatusCodes.Status400BadRequest,
                ErrorMsgUtil.GetBadRequestMsg(nameof(User)), null);
                }

                request.Password = Hash(request.Password);
                User user = _mapper.Map<User>(request);
                await repo.AddAsync(user);
                await _unitOfWork.CompleteAsync();

                UserResponse response = _mapper.Map<UserResponse>(user);
                return new CommonResponse<UserResponse>(StatusCodes.Status201Created,
                    response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new Exception(ErrorMsgUtil.GetErrWhenAdding(nameof(User)));
            }
        }

        public async Task<CommonResponse<UpdateUserDetailResponse>> UpdateUserDetail(Guid userId, UpdateUserDetailRequest request)
        {
            if (userId == Guid.Empty || request == null)
            {
                return new CommonResponse<UpdateUserDetailResponse>(StatusCodes.Status400BadRequest, ErrorMsgUtil.GetInvalidInputMsg(), null);
            }

            try
            {
                var inValids = ValidateUpdateUserDetailRequest(request);
                if (inValids.Count > 0)
                {
                    return new CommonResponse<UpdateUserDetailResponse>(StatusCodes.Status400BadRequest,
                    ErrorMsgUtil.GetBadRequestMsg(nameof(User)), null);
                }

                var user = await _query.GetById(userId);
                if (user == null)
                {
                    return new CommonResponse<UpdateUserDetailResponse>(StatusCodes.Status404NotFound,
                    ErrorMsgUtil.GetCannotFindMsg(nameof(User)), null);
                }

                IRepository<User> repo = _unitOfWork.GetRepository<User>();
                _mapper.Map(request, user);
                repo.Update(user);
                await _unitOfWork.CompleteAsync();
                var response = _mapper.Map<UpdateUserDetailResponse>(user);
                return new CommonResponse<UpdateUserDetailResponse>(StatusCodes.Status200OK, response);
}
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error at ${nameof(UpdateUserDetail)} :" + ex.Message);
                throw new Exception(ErrorMsgUtil.GetErrWhenGetting(nameof(User)));
            }
        }

        private Dictionary<string, string> ValidateUpdateUserDetailRequest(UpdateUserDetailRequest request)
        {
            var inValids = new Dictionary<string, string>();

            if (request.Name.Length > EntityConstantCollection.FreeText100CharMaxLength)
            {
                inValids.Add(JsonPropertyNames.Title, ErrorMsgUtil.GetRequiredFieldMsg(nameof(JsonPropertyNames.Name)));
            }

            else if (request.Title.Length > EntityConstantCollection.FreeText100CharMaxLength)
            {
                inValids.Add(JsonPropertyNames.Title, ErrorMsgUtil.GetRequiredFieldMsg(nameof(JsonPropertyNames.Title)));
            }

            else if (request.Description.Length > EntityConstantCollection.FreeText500CharMaxLength)
            {
                inValids.Add(JsonPropertyNames.Description, ErrorMsgUtil.GetRequiredFieldMsg(nameof(JsonPropertyNames.Description)));
            }

            return inValids;
        }

        private async Task<bool> IsEmailDuplicate(IRepository<User> repo, string email)
        {
            IQueryable<User> users = repo.GetQueryable();
            return await users.AnyAsync(x => x.Email == email);
        }

        public string Hash(string password)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            return passwordHash;
        }

        public bool IsMatch(string hashedPassword, string plainTextPassword)
        {
            bool isMatch = BCrypt.Net.BCrypt.Verify(plainTextPassword, hashedPassword);
            return isMatch;
        }

        public async Task<CommonResponse<UserLoginResponse>> Login(UserLoginRequest request)
        {
            if (request == null)
            {
                return new CommonResponse<UserLoginResponse>(StatusCodes.Status400BadRequest,
                    ErrorMsgUtil.GetInvalidInputMsg(), null);
            }

            try
            {
                User user = await GetUserByEmail(request.Email);
                if (user == null)
                {
                    return new CommonResponse<UserLoginResponse>(StatusCodes.Status422UnprocessableEntity,
                    ErrorMsgUtil.GetUnprocessibleMsg(nameof(User)), null);
                }

                if (IsMatch(user.Password, request.Password) == false)
                {
                    return new CommonResponse<UserLoginResponse>(StatusCodes.Status422UnprocessableEntity,
                    ErrorMsgUtil.GetUnprocessibleMsg(nameof(User)), null);
                }

                string token = GenerateJwtToken(user);
                var loginEntity = _mapper.Map<Login>(token);
                IRepository<Login> repo = _unitOfWork.GetRepository<Login>();

                await repo.AddAsync(loginEntity);
                await _unitOfWork.CompleteAsync();

                return new CommonResponse<UserLoginResponse>(
                StatusCodes.Status200OK,
                "Token acquired",
                new UserLoginResponse(token));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new Exception(ErrorMsgUtil.GetErrWhenAdding(nameof(User)));
            }
        }

        public string JWTToken(User user, string secret, double expiry)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Sid, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Name)
                }),
                Expires = DateTime.UtcNow.AddMinutes(expiry),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private async Task<User> GetUserByEmail(string email)
        {
            IRepository<User> repo = _unitOfWork.GetRepository<User>();
            IQueryable<User> queryable = repo.GetQueryable();
            return await queryable.FirstOrDefaultAsync(x => x.Email == email);
        }

        private string GenerateJwtToken(User user)
        {
            string secret = _configuration.GetConnectionString("TokenSecret");
            double expiry = double.Parse(_configuration.GetConnectionString("TokenExpiryTime"));
            string token = JWTToken(user, secret, expiry);
            return token;
        }

        public Guid ExtractUserIdFromToken(HttpRequest request)
        {
            string token = request.Headers["Authorization"].ToString();
            if (token.Length > 0)
            {
                //keep the token only
                token = token
                    .Replace("Bearer", string.Empty, true, null)
                    .Replace(" ", string.Empty, true, null);

                JwtSecurityToken tokenData = new JwtSecurityTokenHandler().ReadJwtToken(token);
                string userId = tokenData.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value;
                if (Guid.TryParse(userId, out Guid result))
                {
                    return result;
                }
            }
            return Guid.Empty;
        }

        public async Task<CommonResponse<string>> Logout(Guid tokenId)
        {
            if (tokenId == Guid.Empty)
            {
                return new CommonResponse<string>(StatusCodes.Status400BadRequest,
                    ErrorMsgUtil.GetBadRequestMsg(nameof(DataModel.Infrastructure.Models.Login)), null);
            }

            try
            {
                IRepository<Login> repo = _unitOfWork.GetRepository<Login>();
                Login token = await repo.GetByIdAsync(tokenId);
                if (token == null)
                {
                    return new CommonResponse<string>(StatusCodes.Status404NotFound,
                        ErrorMsgUtil.GetCannotFindMsg(nameof(Login)), null);
                }

                repo.HardDelete(token);
                await _unitOfWork.CompleteAsync();
                return new CommonResponse<string>(StatusCodes.Status204NoContent, "Delete a token Successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error at ${nameof(Logout)} :" + ex.Message);
                throw new Exception(ErrorMsgUtil.GetErrWhenGetting(nameof(DataModel.Infrastructure.Models.Login)));
            }
        }
    }
}

