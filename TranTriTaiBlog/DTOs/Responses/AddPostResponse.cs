using System;
using System.Text.Json.Serialization;
using TranTriTaiBlog.Infrastructures.Constants;

namespace TranTriTaiBlog.DTOs.Responses
{
    public class AddPostResponse
    {
        public AddPostResponse()
        {
        }

        public AddPostResponse(Guid postId)
        {
            PostId = postId;
        }

        [JsonPropertyName(JsonPropertyNames.PostId)]
        public Guid PostId { get; set; }

        [JsonPropertyName(JsonPropertyNames.InvalidFields)]
        public IDictionary<string, string> InvalidFields { get; set; }
    }
}

