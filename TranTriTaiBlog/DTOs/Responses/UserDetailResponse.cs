using System;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using TranTriTaiBlog.Infrastructures.Constants;

namespace TranTriTaiBlog.DTOs.Responses
{
    public class UserDetailResponse
    {
        public UserDetailResponse() { }

        public UserDetailResponse(string name, string email, string password) : base()
        {
            Name = name;
            Email = email;
            Password = password;
        }

        [JsonPropertyName(JsonPropertyNames.Name)]
        public string Name { get; set; }

        [JsonPropertyName(JsonPropertyNames.Email)]
        public string Email { get; set; }

        [JsonIgnore]
        [JsonPropertyName(JsonPropertyNames.Password)]
        public string Password { get; set; }

        [JsonPropertyName(JsonPropertyNames.Title)]
        public string Title { get; set; }

        [JsonPropertyName(JsonPropertyNames.Birthday)]
        public string Birthday { get; set; }

        [JsonPropertyName(JsonPropertyNames.Languages)]
        public string Languages { get; set; }

        [JsonPropertyName(JsonPropertyNames.YearExperience)]
        public string YearExperience { get; set; }

        [JsonPropertyName(JsonPropertyNames.Hobbies)]
        public string Hobbies { get; set; }

        [JsonPropertyName(JsonPropertyNames.Description)]
        public string Description { get; set; }
    }
}

