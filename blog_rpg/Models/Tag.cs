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
        [StringLength(30)]
        public string Name { get; set; } = null!;
    }
}
