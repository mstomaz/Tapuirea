using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace blog_rpg.Models
{
    public class Tale
    {
        private Tale() { }
        public Tale(int id, User author, string title, string content, DateTime postDate, DateTime? editDate)
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
        [StringLength(50, ErrorMessage = "O {0} ultrapassa {1} caracteres")]
        [Display(Name = "Título")]
        public string Title { get; set; } = null!;

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
        public IEnumerable<Tag> Tags { get; set; } = new List<Tag>();
    }
}
