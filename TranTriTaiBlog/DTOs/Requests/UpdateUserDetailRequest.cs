using System;
using System.Text.Json.Serialization;
using TranTriTaiBlog.Infrastructures.Constants;

namespace TranTriTaiBlog.DTOs.Requests
{
    public class UpdateUserDetailRequest
    {
        public UpdateUserDetailRequest() { }

        public UpdateUserDetailRequest(string name, string title, string birthday,
            string languages, string yearExperience, string hobbies, string description)
        {
            Name = name;
            Title = title;
            Birthday = birthday;
            Languages = languages;
            YearExperience = yearExperience;
            Hobbies = hobbies;
            Description = description;
        }

        [JsonPropertyName(JsonPropertyNames.Name)]
        public string Name { get; set; }

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

