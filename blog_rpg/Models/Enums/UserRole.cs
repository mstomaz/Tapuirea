using System.ComponentModel.DataAnnotations;

namespace blog_rpg.Models.Enums
{
    public enum UserRole : int
    {
        [Display(Name = "Leitor")]
        Reader = 0,
        [Display(Name = "Autor")]
        Author = 1
    }
}
