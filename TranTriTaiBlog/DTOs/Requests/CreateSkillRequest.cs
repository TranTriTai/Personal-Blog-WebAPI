using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DataModel.Enums;
using TranTriTaiBlog.Infrastructures.Constants;

namespace TranTriTaiBlog.DTOs.Requests
{
    public class CreateSkillRequest
    {
        public CreateSkillRequest() { }

        public CreateSkillRequest(string title)
        {
            Title = title;
        }

        [JsonPropertyName(JsonPropertyNames.Title)]
        public string Title { get; set; }

        [JsonPropertyName(JsonPropertyNames.Domain)]
        public Domain? Domain { get; set; }

        public bool isValid()
        {
            if (Enum.IsDefined(typeof(Domain), Domain))
            {
                return true;
            }
            return false;
        }
    }
}

