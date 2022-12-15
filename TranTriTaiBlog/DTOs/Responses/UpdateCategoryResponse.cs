using System;
using System.Text.Json.Serialization;
using TranTriTaiBlog.Infrastructures.Constants;

namespace TranTriTaiBlog.DTOs.Responses
{
    public class UpdateCategoryResponse
    {
        public UpdateCategoryResponse() { }

        public UpdateCategoryResponse(Guid categoryId)
        {
            CategoryId = categoryId;
        }

        [JsonPropertyName(JsonPropertyNames.CategoryId)]
        public Guid CategoryId { get; set; }

        [JsonPropertyName(JsonPropertyNames.InvalidFields)]
        public IDictionary<string, string> InvalidFields { get; set; }
    }
}
