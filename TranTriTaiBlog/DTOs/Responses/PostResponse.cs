using System;
using System.Text.Json.Serialization;
using TranTriTaiBlog.Infrastructures.Constants;

namespace TranTriTaiBlog.DTOs.Responses
{
    public class PostResponse
    {
        public PostResponse() { }

        public PostResponse(string title, string description, string tag,
            string defaultImageURL, string readingDuration, Guid ownerId, string ownerName,
            Guid categoryId, string categoryName)
        {
            Title = title;
            Description = description;
            Tag = tag;
            DefaultImageUrl = defaultImageURL;
            ReadingDuration = readingDuration;
            OwnerId = ownerId;
            OwnerName = ownerName;
            CategoryId = categoryId;
            CategoryName = categoryName;
        }

        [JsonPropertyName(JsonPropertyNames.Title)]
        public string Title { get; set; }

        [JsonPropertyName(JsonPropertyNames.Description)]
        public string Description { get; set; }

        [JsonPropertyName(JsonPropertyNames.Tag)]
        public string Tag { get; set; }

        [JsonPropertyName(JsonPropertyNames.DefaultImageUrl)]
        public string DefaultImageUrl { get; set; }

        [JsonPropertyName(JsonPropertyNames.ReadingDuration)]
        public string ReadingDuration { get; set; }

        [JsonPropertyName(JsonPropertyNames.OwnerId)]
        public Guid OwnerId { get; set; }

        [JsonPropertyName(JsonPropertyNames.OwnerName)]
        public string OwnerName { get; set; }

        [JsonPropertyName(JsonPropertyNames.CategoryId)]
        public Guid CategoryId { get; set; }

        [JsonPropertyName(JsonPropertyNames.CategoryName)]  
        public string CategoryName { get; set; }
    }
}

