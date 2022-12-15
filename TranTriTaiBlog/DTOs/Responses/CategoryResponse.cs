using System;
using System.Text.Json.Serialization;
using TranTriTaiBlog.Infrastructures.Constants;

namespace TranTriTaiBlog.DTOs.Responses
{
    public class CategoryResponse
    {
        public CategoryResponse() { }

        public CategoryResponse(string category)
        {
            Category = category;
        }

        [JsonPropertyName(JsonPropertyNames.Category)]
        public string Category { get; set; }
    }
}

