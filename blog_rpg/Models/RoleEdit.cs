using Microsoft.AspNetCore.Identity;
using blog_rpg.Areas.Identity.Models;

namespace blog_rpg.Models
{
    public class RoleEdit
    {
        public IdentityRole Role { get; set; } = null!;
        public IEnumerable<ApplicationUser> Members { get; set; } = [];
        public IEnumerable<ApplicationUser> NonMembers { get; set; } = [];

    }
}
