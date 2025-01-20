using System.ComponentModel.DataAnnotations;

namespace blog_rpg.Models
{
    public class Tag
    {
        public Tag() { }

        public Tag(int id, string name) 
        {
            Id = id;
            Name = name;
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
