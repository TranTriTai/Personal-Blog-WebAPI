using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TranTriTaiBlog.Infrastructures.Constants;

namespace TranTriTaiBlog.DTOs.Responses
{
    public class UpdateUserDetailResponse
    {
        public UpdateUserDetailResponse() { }

        [JsonPropertyName(JsonPropertyNames.UserId)]
        public Guid UserId { get; set; }

        [JsonPropertyName(JsonPropertyNames.InvalidFields)]
        public IDictionary<string, string> InvalidFields { get; set; }
    }
}

