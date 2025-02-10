using System.ComponentModel.DataAnnotations;

namespace blog_rpg.Models.ViewModel
{
    public class UpdateUserViewModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "O campo '{0}' é obrigatório")]
        public string? Email { get; set; }
    }
}
