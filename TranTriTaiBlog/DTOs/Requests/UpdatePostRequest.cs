using System;
using System.Text.Json.Serialization;
using TranTriTaiBlog.Infrastructures.Constants;

namespace TranTriTaiBlog.DTOs.Requests
{
    public class UpdatePostRequest
    {
        public UpdatePostRequest() { }

        public UpdatePostRequest(string title, string description,
            string tags, string image, string timeToRead, Guid categoryId)
        {
            Title = title;
            Description = description;
            Tags = tags;
            Image = image;
            TimeToRead = timeToRead;
            CategoryId = categoryId;
        }

        [JsonPropertyName(JsonPropertyNames.Title)]
        public string Title { get; set; }

        [JsonPropertyName(JsonPropertyNames.Description)]
        public string Description { get; set; }

        [JsonPropertyName(JsonPropertyNames.Tags)]
        public string Tags { get; set; }

        [JsonPropertyName(JsonPropertyNames.Image)]
        public string Image { get; set; }

        [JsonPropertyName(JsonPropertyNames.TimeToRead)]
        public string TimeToRead { get; set; }

        [JsonPropertyName(JsonPropertyNames.CategoryId)]
        public Guid CategoryId { get; set; }
    }
}

