using System;
using DataModel.Enums;
using System.Text.Json.Serialization;
using TranTriTaiBlog.Infrastructures.Constants;

namespace TranTriTaiBlog.DTOs.Requests
{
    public class UpdateSkillRequest
    {
        public UpdateSkillRequest() { }

        public UpdateSkillRequest(string title, Domain domain)
        {
            Title = title;
            Domain = domain;
        }

        [JsonPropertyName(JsonPropertyNames.Title)]
        public string Title { get; set; }

        [JsonPropertyName(JsonPropertyNames.Domain)]
        public Domain? Domain { get; set; }
    }
}

