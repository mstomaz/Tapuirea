using System.ComponentModel.DataAnnotations;

namespace blog_rpg.Models
{
    public class TagDTO
    {
        public TagDTO(int id, string name)
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

        public static implicit operator TagDTO(Tag model)
        {
            return new TagDTO(model.Id, model.Name);
        }
    }
}
