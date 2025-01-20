namespace blog_rpg.Models
{
    public class Tale
    {
        public Tale() { }
        public Tale(int id, User author, string title, string content, DateTime postDate, DateTime editDate) 
        {
            Id = id;
            Author = author;
            Title = title;
            Content = content;
            PostDate = postDate;
            EditDate = editDate;
        }

        public int Id { get; set; }
        public User Author { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime EditDate { get; set; }
    }
}
