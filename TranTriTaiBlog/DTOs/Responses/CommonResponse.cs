﻿using System;
using System.Text.Json.Serialization;
using TranTriTaiBlog.Infrastructures.Constants;

namespace TranTriTaiBlog.DTOs.Responses
{
    public class CommonResponse<T> where T : class
    {
        public CommonResponse() { }

        public CommonResponse(int statusCode)
        {
            StatusCode = statusCode;
            Message = null;
            Result = null;
        }

        public CommonResponse(int statusCode, T result) : this(statusCode)
        {
            Message = "Ok";
            Result = result;
        }

        public CommonResponse(int statusCode, string message, T result) : this(statusCode)
        {
            Message = message;
            Result = result;
        }

        [JsonPropertyName(JsonPropertyNames.StatusCode)]
        public int StatusCode { get; set; }

        [JsonPropertyName(JsonPropertyNames.Message)]
        public string Message { get; set; }

        [JsonPropertyName(JsonPropertyNames.Result)]
        public T Result { get; set; }
    }
}

