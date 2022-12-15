using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TranTriTaiBlog.Infrastructures.Constants;

namespace TranTriTaiBlog.DTOs.Requests
{
    public class UserRegistrationRequest
    {
        [Required]
        [JsonPropertyName(JsonPropertyNames.Name)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(225)]
        [JsonPropertyName(JsonPropertyNames.Email)]
        public string Email { get; set; }

        [Required]
        [JsonPropertyName(JsonPropertyNames.Password)]
        public string Password { get; set; }
    }
}

