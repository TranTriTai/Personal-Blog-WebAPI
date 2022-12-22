using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataModel.Enums;
using DataModel.Infrastructure.Interfaces;
using DataModel.Infrastructure.Interfaces.Query;
using DataModel.Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TranTriTaiBlog.DTOs.Requests;
using TranTriTaiBlog.DTOs.Responses;
using TranTriTaiBlog.Infrastructures.Constants;
using TranTriTaiBlog.Infrastructures.Helper.Constants;
using TranTriTaiBlog.Infrastructures.Helper.MessageUtil;
using TranTriTaiBlog.Infrastructures.Intefaces;
using TranTriTaiBlog.Infrastructures.Services.UserServices;

namespace TranTriTaiBlog.Infrastructures.Services
{
    public class SkillService : ISkillService 
    {
        private readonly IMapper _mapper;
        private readonly ILogger<SkillService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISkillQuery _query;

        public SkillService(IMapper mapper, ILogger<SkillService> logger,
            IUnitOfWork unitOfWork, ISkillQuery query)
        {
            _mapper = mapper;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _query = query;
        }

        public async Task<CommonResponse<CreateSkillResponse>> CreateSkill(CreateSkillRequest request)
        {
            if (request == null
                || Enum.IsDefined(typeof(Domain), request?.Domain) == false
                || request.Title == null)
            {
                return new CommonResponse<CreateSkillResponse>(StatusCodes.Status400BadRequest,
                    ErrorMsgUtil.GetBadRequestMsg(nameof(Skill)), null);
            }

            try
            {
                var inValids = ValidateCreateSkillRequest(request);
                if (inValids.ContainsKey(JsonPropertyNames.Title))
                {
                    return new CommonResponse<CreateSkillResponse>(StatusCodes.Status400BadRequest,
                    ErrorMsgUtil.GetBadRequestMsg(nameof(Skill)), null);
                }

                var skill = await _query.GetListSkills();

                if (skill.Any(x => x.Title == request.Title))
                {
                    return new CommonResponse<CreateSkillResponse>(StatusCodes.Status422UnprocessableEntity,
                    ErrorMsgUtil.GetUnprocessibleMsg(nameof(Skill)), null);
                }

                var skillEntity = _mapper.Map<Skill>(request);
                IRepository<Skill> repo = _unitOfWork.GetRepository<Skill>();

                await repo.AddAsync(skillEntity);
                await _unitOfWork.CompleteAsync();

                return new CommonResponse<CreateSkillResponse>(StatusCodes.Status201Created,
                    _mapper.Map<CreateSkillResponse>(skillEntity));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new Exception(ErrorMsgUtil.GetErrWhenAdding(nameof(Skill)));
            }
        }

        public async Task<CommonResponse<string>> DeleteSkill(Guid skillId)
        {
            if (skillId == Guid.Empty)
            {
                return new CommonResponse<string>(StatusCodes.Status400BadRequest,
                    ErrorMsgUtil.GetBadRequestMsg(nameof(Skill)), null);
            }
            try
            {
                var skill = await _query.GetById(skillId);
                if (skill == null)
                {
                    return new CommonResponse<string>(StatusCodes.Status404NotFound,
                        ErrorMsgUtil.GetCannotFindMsg(nameof(Skill)), null);
                }

                IRepository<Skill> repo = _unitOfWork.GetRepository<Skill>();

                repo.HardDelete(skill);
                await _unitOfWork.CompleteAsync();

                return new CommonResponse<string>(StatusCodes.Status204NoContent, "Delete a skill Successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error at ${nameof(DeleteSkill)} :" + ex.Message);
                throw new Exception(ErrorMsgUtil.GetErrWhenGetting(nameof(Skill)));
            }
        }

        public async Task<CommonResponse<SkillsResponse[]>> GetListSkills()
        {
            try
            {
                var skillList = await _query.GetListSkills();
                if (skillList.Length == 0)
                {
                    return new CommonResponse<SkillsResponse[]>(StatusCodes.Status204NoContent);
                }

                var responce = _mapper.Map<SkillsResponse[]>(skillList);
                return new CommonResponse<SkillsResponse[]>(StatusCodes.Status200OK, responce);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error at ${nameof(GetListSkills)} :" + ex.Message);
                throw new Exception(ErrorMsgUtil.GetErrWhenGetting(nameof(Skill)));
            }
            
        }

        public async Task<CommonResponse<UpdateSkillResponse>> UpdateSkill(Guid skillId, UpdateSkillRequest request)
        {
            if ( skillId == Guid.Empty || request == null)
            {
                return new CommonResponse<UpdateSkillResponse>(StatusCodes.Status400BadRequest, ErrorMsgUtil.GetInvalidInputMsg(), null);
            }

            try
            {
                var inValids = ValidateUpdateSkillRequest(request);
                if (inValids.Count() > 0 || Enum.IsDefined(typeof(Domain), request?.Domain) == false)
                {
                    return new CommonResponse<UpdateSkillResponse>(StatusCodes.Status400BadRequest,
                    ErrorMsgUtil.GetBadRequestMsg(nameof(Skill)), null);
                }

                var skill = await _query.GetById(skillId);
                if (skill == null)
                {
                    return new CommonResponse<UpdateSkillResponse>(StatusCodes.Status404NotFound,
                    ErrorMsgUtil.GetCannotFindMsg(nameof(Skill)), null);
                }

                IRepository<Skill> repo = _unitOfWork.GetRepository<Skill>();
                _mapper.Map(request, skill);
                repo.Update(skill);
                await _unitOfWork.CompleteAsync();
                var response = _mapper.Map<UpdateSkillResponse>(skill);
                return new CommonResponse<UpdateSkillResponse>(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error at ${nameof(GetListSkills)} :" + ex.Message);
                throw new Exception(ErrorMsgUtil.GetErrWhenGetting(nameof(Skill)));
            }
        }

        public Dictionary<string, string> ValidateCreateSkillRequest(CreateSkillRequest request)
        {
            var inValids = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(request.Title))
            {
                inValids.Add(JsonPropertyNames.Title, ErrorMsgUtil.GetRequiredFieldMsg(nameof(JsonPropertyNames.Title)));
            }

            else if (request.Title.Length > EntityConstantCollection.FreeText100CharMaxLength)
            {
                inValids.Add(JsonPropertyNames.Title, ErrorMsgUtil.GetRequiredFieldMsg(nameof(JsonPropertyNames.Title)));
            }

            return inValids;
        }

        private Dictionary<string, string> ValidateUpdateSkillRequest(UpdateSkillRequest request)
        {
            var inValids = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(request.Title))
            {
                inValids.Add(JsonPropertyNames.Title, ErrorMsgUtil.GetRequiredFieldMsg(nameof(JsonPropertyNames.Title)));
            }

            else if (request.Title.Length > EntityConstantCollection.FreeText100CharMaxLength)
            {
                inValids.Add(JsonPropertyNames.Title, ErrorMsgUtil.GetRequiredFieldMsg(nameof(JsonPropertyNames.Title)));
            }

            return inValids;
        }
    }
}

