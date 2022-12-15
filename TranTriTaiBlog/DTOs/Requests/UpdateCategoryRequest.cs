using System;
using System.Text.Json.Serialization;
using TranTriTaiBlog.Infrastructures.Constants;

namespace TranTriTaiBlog.DTOs.Requests
{
    public class UpdateCategoryRequest
    {
        public UpdateCategoryRequest() { }

        public UpdateCategoryRequest(string category)
        {
            Category = category;
        }

        [JsonPropertyName(JsonPropertyNames.Category)]
        public string Category { get; set; }
    }
}

