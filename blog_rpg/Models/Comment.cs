namespace blog_rpg.Models
{
    public class Comment
    {
        public Comment() { }
        public Comment(int id, User author, string content, DateTime postDate, DateTime editDate)
        {
            Id = id;
            Author = author;
            Content = content;
            PostDate = postDate;
            EditDate = editDate;
        }

        public int Id { get; init; }
        public User? Author { get; set; }
        public int AuthorId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime PostDate { get; init; }
        public DateTime EditDate { get; private set; }
    }
}
