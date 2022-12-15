using System;
using System.Text.Json.Serialization;
using TranTriTaiBlog.Infrastructures.Constants;

namespace TranTriTaiBlog.DTOs.Responses
{
    public class ListPostResponse
    {
        [JsonPropertyName(JsonPropertyNames.Posts)]
        public PostResponse[] Posts { get; set; }

        [JsonPropertyName(JsonPropertyNames.Pagination)]
        public PaginationResponse Pagination { get; set; }
    }
}

