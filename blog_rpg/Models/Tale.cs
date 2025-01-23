using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace blog_rpg.Models
{
    public class Tale
    {
        private Tale() { }
        public Tale(int id, User? author, string title, string content, DateTime postDate, DateTime? editDate)
        {
            Id = id;
            Author = author;
            Title = title;
            Content = content;
            PostDate = postDate;
            EditDate = editDate;
        }

        [Required]
        public int Id { get; set; }
        public User? Author { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; } = null!;

        [Required]
        public string Content { get; set; } = null!;

        [Required]
        public DateTime PostDate { get; set; }

        public DateTime? EditDate { get; set; }
        public IEnumerable<Tag> Tags { get; set; } = new List<Tag>();
    }
}
