using System.ComponentModel.DataAnnotations;

namespace blog_rpg.Models
{
    public class CommentDTO
    {
        public CommentDTO(int id, UserDTO? author, string content, DateTime postDate, DateTime? editDate)
        {
            Id = id;
            Author = author;
            Content = content;
            PostDate = postDate;
            EditDate = editDate;
        }

        [Required]
        public int Id { get; set; }
        public UserDTO? Author { get; set; }

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

        public static explicit operator CommentDTO(Comment model)
        {
            return new CommentDTO(
                model.Id,
                model.Author is not null ? (UserDTO)model.Author : null,
                model.Content,
                model.PostDate,
                model.EditDate);
        }
    }
}
