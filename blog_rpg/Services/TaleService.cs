using blog_rpg.Data;
using blog_rpg.Models;
using Microsoft.EntityFrameworkCore;

namespace blog_rpg.Services
{
    public class TaleService
    {
        private readonly BlogContext _context;

        public TaleService(BlogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tale>> GetAllAsync()
        {
            return await _context.Tales.ToListAsync();
        }
    }
}
