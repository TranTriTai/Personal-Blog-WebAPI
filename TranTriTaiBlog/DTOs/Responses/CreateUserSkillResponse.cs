using System;
using DataModel.Enums;
using System.Text.Json.Serialization;
using TranTriTaiBlog.Infrastructures.Constants;

namespace TranTriTaiBlog.DTOs.Responses
{
    public class CreateUserSkillsResponse
    {
        public CreateUserSkillsResponse() { }

        public CreateUserSkillsResponse(Guid userSkills)
        {
            UserSkillId = userSkills;
        }

        [JsonPropertyName(JsonPropertyNames.UserSkillId)]
        public Guid UserSkillId { get; set; }
    }
}

