using System;
using System.Text.Json.Serialization;
using DataModel.Enums;
using TranTriTaiBlog.Infrastructures.Constants;

namespace TranTriTaiBlog.DTOs.Responses
{
    public class SkillsResponse
    {
        public SkillsResponse() { }

        public SkillsResponse(Guid id, string title, Domain domain)
        {
            SkillId = id;
            Title = title;
            Domain = domain;
        }

        [JsonPropertyName(JsonPropertyNames.SkillId)]
        public Guid SkillId { get; set; }

        [JsonPropertyName(JsonPropertyNames.Title)]
        public string Title { get; set; }

        [JsonPropertyName(JsonPropertyNames.Domain)]
        public Domain Domain { get; set; }
    }
}

