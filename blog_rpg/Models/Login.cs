using System.ComponentModel.DataAnnotations;

namespace blog_rpg.Models
{
    public class Login
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string? Email {  get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public string? ReturnUrl { get; set; }
        public bool Remember { get; set; }
    }
}
