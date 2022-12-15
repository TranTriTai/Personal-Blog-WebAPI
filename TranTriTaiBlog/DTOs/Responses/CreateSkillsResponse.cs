using System;
using System.Text.Json.Serialization;
using TranTriTaiBlog.Infrastructures.Constants;

namespace TranTriTaiBlog.DTOs.Responses
{
    public class CreateSkillResponse
    {
        public CreateSkillResponse()
        {
        }

        [JsonPropertyName(JsonPropertyNames.SkillId)]
        public Guid SkillId { get; set; }

        [JsonPropertyName(JsonPropertyNames.InvalidFields)]
        public IDictionary<string, string> InvalidFields { get; set; }
    }
}

