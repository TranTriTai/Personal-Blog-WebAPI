using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TranTriTaiBlog.Infrastructures.Constants;

namespace TranTriTaiBlog.DTOs.Responses
{
    public class CreateCategoryResponse
    {
        public CreateCategoryResponse()
        {
        }

        [JsonPropertyName(JsonPropertyNames.CategoryId)]
        public Guid CategoryId { get; set; }

        [JsonPropertyName(JsonPropertyNames.InvalidFields)]
        public IDictionary<string, string> InvalidFields { get; set; }
    }
}

