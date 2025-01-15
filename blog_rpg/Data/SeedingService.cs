using blog_rpg.Models;

namespace blog_rpg.Data
{
    public class SeedingService
    {
        private readonly BlogContext _context;

        public SeedingService(BlogContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context is null)
                return;

            if (!_context.Users.Any())
            {
                User u1 = new User(1, "Mateus", Models.Enums.UserRole.Author);
                _context.Users.Add(u1);
                _context.SaveChanges();
            }
        }
    }
}
