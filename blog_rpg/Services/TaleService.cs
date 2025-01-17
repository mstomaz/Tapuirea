using blog_rpg.Data;
using blog_rpg.Models;

namespace blog_rpg.Services
{
    public class TaleService
    {
        private readonly BlogContext _context;

        public TaleService(BlogContext context)
        {
            _context = context;
        }

        public IEnumerable<Tale> GetAll()
        {
            return _context.Tales;
        }
    }
}
