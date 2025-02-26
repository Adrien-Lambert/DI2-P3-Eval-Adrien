using backend.Logic.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace backend.Data.DTO
{
    public class ApplicationReadDto
    {
        [JsonPropertyName("application_id")]
        public int ApplicationId { get; set; }

        [JsonPropertyName("application_name")]
        public string ApplicationName { get; set; }

        [Required]
        [JsonPropertyName("application_type")]
        public ApplicationType ApplicationType { get; set; }
    }
}
