using System;
using System.Text.Json.Serialization;
using TranTriTaiBlog.DTOs.Requests;
using TranTriTaiBlog.Infrastructures.Constants;

namespace TranTriTaiBlog.DTOs.Responses
{
    public class PaginationResponse
    {
        public PaginationResponse(PaginationRequest request, int totalResult)
        {
            PageNum = request.Page;
            PageSize = request.Size;
            TotalResult = totalResult;
        }

        [JsonPropertyName(JsonPropertyNames.PageNum)]
        public int PageNum { get; set; }

        [JsonPropertyName(JsonPropertyNames.PageSize)]
        public int PageSize { get; set; }

        [JsonPropertyName(JsonPropertyNames.TotalResult)]
        public int TotalResult { get; set; }
    }
}

