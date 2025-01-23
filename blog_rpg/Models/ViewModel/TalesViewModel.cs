namespace blog_rpg.Models.ViewModel
{
    public class TalesViewModel
    {
        public UserDTO User { get; set; } = null!;
        public IEnumerable<TaleDTO> Tales { get; set; } = null!;
    }
}
