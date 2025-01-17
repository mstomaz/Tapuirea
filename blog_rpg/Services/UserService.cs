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

        public User Get(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }
    }
}
