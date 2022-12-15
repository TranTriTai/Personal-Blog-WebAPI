using System;
using System.Text.Json.Serialization;
using TranTriTaiBlog.Infrastructures.Constants;

namespace TranTriTaiBlog.DTOs.Responses
{
    public class UserResponse
    {
        [JsonPropertyName(JsonPropertyNames.Id)]
        public Guid Id { get; set; }

        [JsonPropertyName(JsonPropertyNames.Name)]
        public string Name { get; set; }

        [JsonPropertyName(JsonPropertyNames.Email)]
        public string Email { get; set; }
    }
}

