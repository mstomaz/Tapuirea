using blog_rpg.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace blog_rpg.Models
{
    public class UserDTO
    {
        public UserDTO() { }
        public UserDTO(string id, string name, string email, UserRole role, Country country, DateTime birthDate)
        {
            Id = id;
            Name = name;
            Email = email;
            Role = role;
            Country = country;
            BirthDate = birthDate;
            Age = GetInitialAge();
        }

        [Required]
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo '{0}' não pode ser vazio")]
        [Display(Name = "Nome")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O {0} deve ter entre {2} e {1} caracteres.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo '{0}' não pode ser vazio")]
        [Display(Name = "Senha")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo '{0}' não pode ser vazio")]
        [RegularExpression("^[a-zA-Z]{1}[\\.\\w-_]+@[a-z[A-Z]+\\.com", ErrorMessage = "Email inválido.")]
        public string? Email { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Permissões")]
        public UserRole Role { get; }

        [Required]
        public Country Country { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public int Age { get; private set; }

        private int GetInitialAge()
        {
            if (BirthDate.Ticks == 0)
                throw new InvalidOperationException("A data de nascimento do usuário não foi definida corretamente. Impossível calcular a idade.");
            int age = DateTime.Today.Year - BirthDate.Year;
            return DateTime.Today < BirthDate.AddYears(age) ? age-- : age;
        }

        public static explicit operator UserDTO(User model)
        {
            return new UserDTO(model.Id, model.Name, model.Email, model.Role, model.Country, model.BirthDate);
        }
    }
}
