using System.ComponentModel.DataAnnotations;

namespace blog_rpg.Models
{
    public class Comment
    {
        private Comment() { }
        public Comment(int id, User author, string content, DateTime postDate, DateTime? editDate)
        {
            Id = id;
            Author = author;
            Content = content;
            PostDate = postDate;
            EditDate = editDate;
        }

        [Required]
        public int Id { get; set; }
        public User? Author { get; set; }

        [Required]
        public string AuthorId { get; set; }

        [Required]
        public string Content { get; set; } = null!;

        [Required]
        public DateTime PostDate { get; set; }

        public DateTime? EditDate { get; set; }
    }
}
