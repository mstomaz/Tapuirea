using blog_rpg.Data;
using blog_rpg.Models;

namespace blog_rpg.Services
{
    public class UserService
    {
        private readonly BlogContext _context;

        public UserService(BlogContext context)
        {
            _context = context;
        }

        public async Task<User> GetAsync(string id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}
