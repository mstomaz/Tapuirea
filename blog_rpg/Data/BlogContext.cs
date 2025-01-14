using Microsoft.EntityFrameworkCore;
using blog_rpg.Models;
using blog_rpg.Models.Enums;

namespace blog_rpg.Data
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }

        public DbSet<Tale> Tales { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().Property(u => u.Role).HasDefaultValue(UserRole.Reader);
        }
    }
}
