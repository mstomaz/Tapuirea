using blog_rpg.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace blog_rpg.Models
{
    public class UserDTO
    {
        public UserDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }

        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nome")]
        [StringLength(100, ErrorMessage = "O {0} ultrapassa {1} caracteres")]
        public string Name { get; set; } = null!;

        public static implicit operator UserDTO(User model)
        {
            return new UserDTO(model.Id, model.Name);
        }
    }
}
