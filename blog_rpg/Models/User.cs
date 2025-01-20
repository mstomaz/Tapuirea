using blog_rpg.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace blog_rpg.Models
{
    public class User
    {
        public User(int id, string name, UserRole role)
        {
            Id = id;
            Name = name;
            Role = role;
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public UserRole Role { get; set; }
    }
}
