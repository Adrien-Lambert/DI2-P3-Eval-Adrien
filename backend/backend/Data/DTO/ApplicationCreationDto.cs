using backend.Logic.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace backend.Data.DTO
{
    public class ApplicationCreationDto
    {
        [Required]
        [MaxLength(50)]
        [JsonPropertyName("application_name")]
        public string ApplicationName { get; set; }

        [Required]
        [JsonPropertyName("application_type")]
        public ApplicationType ApplicationType { get; set; }
    }
}
