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
        public int AuthorId { get; set; }

        [Required]
        [Display(Name = "Conteúdo")]
        public string Content { get; set; } = null!;

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Data de postagem")]
        public DateTime PostDate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Editado em:")]
        public DateTime? EditDate { get; set; }
    }
}
