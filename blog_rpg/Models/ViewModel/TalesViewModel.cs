namespace blog_rpg.Models.ViewModel
{
    public class TalesViewModel
    {
        public User User { get; set; }
        public IEnumerable<Tale> Tales { get; set; }
    }
}
