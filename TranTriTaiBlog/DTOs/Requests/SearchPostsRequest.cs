using System;
using System.Text.Json.Serialization;
using TranTriTaiBlog.Infrastructures.Constants;

namespace TranTriTaiBlog.DTOs.Requests
{
    public class SearchPostsRequest : PaginationRequest
    {
        public SearchPostsRequest() { }

        public SearchPostsRequest(string title, string description, string tag,
            string readingDuration)
        {
            Title = title;
            Description = description;
            Tag = tag;
            ReadingDuration = readingDuration;
        }

        [JsonPropertyName(JsonPropertyNames.Title)]
        public string Title { get; set; }

        [JsonPropertyName(JsonPropertyNames.Description)]
        public string Description { get; set; }

        [JsonPropertyName(JsonPropertyNames.Tag)]
        public string Tag { get; set; }

        [JsonPropertyName(JsonPropertyNames.ReadingDuration)]
        public string ReadingDuration { get; set; }
    }
}

