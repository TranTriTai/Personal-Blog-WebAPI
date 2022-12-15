using System;
using System.Text.Json.Serialization;
using TranTriTaiBlog.Infrastructures.Constants;

namespace TranTriTaiBlog.DTOs.Requests
{
    public class CreateCategoryRequest
    {
        public CreateCategoryRequest() { }

        public CreateCategoryRequest(string category)
        {
            Category = category;
        }

        [JsonPropertyName(JsonPropertyNames.Category)]
        public string Category { get; set; }
    }
}

