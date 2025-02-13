using System.ComponentModel.DataAnnotations;

namespace blog_rpg.Models.ViewModel
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "O campo '{0}' é obrigatório")]
        [Display(Name = "Nome")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O {0} deve ter entre {2} e {1} caracteres.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "O campo '{0}' é obrigatório")]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "O campo '{0}' é obrigatório")]
        [RegularExpression("^[a-zA-Z]{1}[\\.\\w-_]+@[a-z[A-Z]+\\.com", ErrorMessage = "Email inválido.")]
        public string? Email { get; set; } = null!;
    }
}
