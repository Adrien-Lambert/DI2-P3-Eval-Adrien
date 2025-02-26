using backend.Logic.Enums;
using System.ComponentModel.DataAnnotations;

namespace backend.Logic.Models
{
    public class Application
    {
        [Key]
        public int ApplicationId { get; set; }

        [Required]
        [MaxLength(50)]
        public string ApplicationName { get; set; }

        [Required]
        public ApplicationType ApplicationType { get; set; }

        public List<Password> Passwords { get; set; } = new();

    }
}
