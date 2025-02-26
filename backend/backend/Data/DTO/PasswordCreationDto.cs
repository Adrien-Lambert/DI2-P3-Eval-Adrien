using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace backend.Data.DTO
{
    public class PasswordCreationDto
    {
        [Required]
        [JsonPropertyName("account_name")]
        public string AccountName { get; set; }

        [Required]
        [JsonPropertyName("password")]
        public string EncryptedPassword { get; set; }

        [Required]
        [JsonPropertyName("application_id")]
        public int ApplicationId { get; set; }
    }
}
