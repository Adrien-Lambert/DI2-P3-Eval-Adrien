using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace backend.Data.DTO
{
    public class PasswordReadDto
    {
        [JsonPropertyName("password_id")]
        public int PasswordId { get; set; }

        [JsonPropertyName("account_name")]
        public string AccountName { get; set; }

        [JsonPropertyName("password")]
        public string EncryptedPassword { get; set; }

        [JsonPropertyName("application")]
        public ApplicationReadDto Application { get; set; }
    }
}
