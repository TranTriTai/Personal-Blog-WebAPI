using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TranTriTaiBlog.Infrastructures.Constants;

namespace TranTriTaiBlog.DTOs.Responses
{
    public class UpdateSkillResponse
    {
        public UpdateSkillResponse() { }

        public UpdateSkillResponse(Guid skillId)
        {
            SkillId = skillId;
        }

        [JsonPropertyName(JsonPropertyNames.SkillId)]
        public Guid SkillId { get; set; }

        [JsonPropertyName(JsonPropertyNames.InvalidFields)]
        public IDictionary<string, string> InvalidFields { get; set; }
    }
}

