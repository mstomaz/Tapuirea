using blog_rpg.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace blog_rpg.Models
{
    public class User
    {
        private User() { }
        public User(int id, string name, UserRole role)
        {
            Id = id;
            Name = name;
            Role = role;
        }

        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;


        [Required]
        public UserRole Role { get; set; }
    }
}
