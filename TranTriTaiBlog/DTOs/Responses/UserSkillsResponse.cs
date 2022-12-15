using System;
using System.Text.Json.Serialization;
using DataModel.Enums;
using TranTriTaiBlog.Infrastructures.Constants;

namespace TranTriTaiBlog.DTOs.Responses
{
    public class UserSkillsResponse
    {
        public UserSkillsResponse() { }

        public UserSkillsResponse(Guid skillId)
        {
            SkillId = skillId;
        }

        [JsonPropertyName(JsonPropertyNames.UserSkillId)]
        public Guid SkillId { get; set; }
    }
}

