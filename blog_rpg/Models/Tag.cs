using System.ComponentModel.DataAnnotations;

namespace blog_rpg.Models
{
    public class Tag
    {
        private Tag() { }

        public Tag(int id, string name) 
        {
            Id = id;
            Name = name;
        }

        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "O {0} ultrapassa {1} caracteres")]
        [Display(Name = "Nome")]
        public string Name { get; set; } = null!;
    }
}
