using blog_rpg.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace blog_rpg.Models
{
    public class User
    {
        private User() { }
        public User(string id, string name, UserRole role, Country country, DateTime birthDate)
        {
            Id = id;
            Name = name;
            Role = role;
            Country = country;
            BirthDate = birthDate;
            Age = GetInitialAge();
        }

        [Required]
        public string Id { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        [RegularExpression("^[a-zA-Z]{1}[\\.\\w-_]+@[a-z[A-Z]+\\.com", ErrorMessage = "Email inválido")]
        public string Email { get; set; } = string.Empty;

        [Required]
        public UserRole Role { get; set; }

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
    }
}
