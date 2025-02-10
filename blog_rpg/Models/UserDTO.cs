using blog_rpg.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace blog_rpg.Models
{
    public class UserDTO
    {
        public UserDTO() { }
        public UserDTO(string id, string name, string email, UserRole role)
        {
            Id = id;
            Name = name;
            Email = email;
            Role = role;
        }

        [Required]
        public string Id { get; set; } = null!;

        [Required(ErrorMessage = "O campo '{0}' não pode ser vazio")]
        [Display(Name = "Nome")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O {0} deve ter entre {2} e {1} caracteres.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "O campo '{0}' não pode ser vazio")]
        [Display(Name = "Senha")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "O campo '{0}' não pode ser vazio")]
        [RegularExpression("^[a-zA-Z]{1}[\\.\\w-_]+@[a-z[A-Z]+\\.com", ErrorMessage = "Email inválido.")]
        public string? Email { get; set; } = null!;

        [Required]
        [Display(Name = "Permissões")]
        public UserRole Role { get; }

        public static explicit operator UserDTO(User model)
        {
            return new UserDTO(model.Id, model.Name, model.Email, model.Role);
        }
    }
}
