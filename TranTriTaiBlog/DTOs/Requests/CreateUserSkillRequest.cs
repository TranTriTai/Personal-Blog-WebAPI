using System;
using System.Text.Json.Serialization;
using DataModel.Enums;
using TranTriTaiBlog.Infrastructures.Constants;

namespace TranTriTaiBlog.DTOs.Requests
{
    public class CreateUserSkillRequest
    {
        public CreateUserSkillRequest() { }

        public CreateUserSkillRequest(Guid skillId, SkillLevel level)
        {
            SkillId = skillId;
            Level = level;
        }

        [JsonPropertyName(JsonPropertyNames.SkillId)]
        public Guid SkillId { get; set; }

        [JsonPropertyName(JsonPropertyNames.Level)]
        public SkillLevel? Level { get; set; }

        public bool isValid()
        {
            if(Enum.IsDefined(typeof(SkillLevel), Level))
            {
                return true;
            }
            return false;
        }
    }
}

