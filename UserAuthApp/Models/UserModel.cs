using System.ComponentModel.DataAnnotations;

namespace UserAuthApp.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string? Username { get; set; }

        [Required]
        [StringLength(255)]
        public string? PasswordHash { get; set; }
    }
}
