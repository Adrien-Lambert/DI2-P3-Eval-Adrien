using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Logic.Models
{
    public class Password
    {
        [Key]
        public int PasswordId { get; set; }

        [Required]
        public string AccountName { get; set; }

        [Required]
        public string EncryptedPassword { get; set; }

        [Required]
        public int ApplicationId { get; set; }

        [ForeignKey("ApplicationId")]
        public Application Application { get; set; }
    }
}
