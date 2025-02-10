using blog_rpg.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace blog_rpg.Models
{
    public class User
    {
        private User() { }
        public User(string id, string name, UserRole role)
        {
            Id = id;
            Name = name;
            Role = role;
        }

        [Required]
        public string Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        [RegularExpression("^[a-zA-Z]{1}[\\.\\w-_]+@[a-z[A-Z]+\\.com", ErrorMessage = "Email inválido")]
        public string Email { get; set; } = null!;


        [Required]
        public UserRole Role { get; set; }
    }
}
