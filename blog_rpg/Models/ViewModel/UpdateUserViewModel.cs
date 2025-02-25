using blog_rpg.CustomAttributes;
using blog_rpg.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace blog_rpg.Models.ViewModel
{
    public class UpdateUserViewModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "O campo '{0}' é obrigatório")]
        [RegularExpression("^[a-zA-Z]{1}[\\.\\w-_]+@[a-z[A-Z]+\\.com", ErrorMessage = "Email inválido.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "O campo '{0}' é obrigatório")]
        [Display(Name = "Nacionalidade")]
        public Country Country { get; set; }

        [Required(ErrorMessage = "O campo '{0}' é obrigatório")]
        [Display(Name = "Data de nascimento")]
        [DataType(DataType.Date)]
        [BirthDateValidator]
        public DateTime BirthDate { get; init; }
    }
}
