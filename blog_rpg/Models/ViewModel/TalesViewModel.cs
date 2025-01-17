namespace blog_rpg.Models.ViewModel
{
    public class TalesViewModel
    {
        public User User { get; set; } = null!;
        public IEnumerable<Tale> Tales { get; set; } = null!;
    }
}
