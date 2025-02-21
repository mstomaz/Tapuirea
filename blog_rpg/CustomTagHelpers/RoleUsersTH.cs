using blog_rpg.Areas.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;

namespace blog_rpg.CustomTagHelpers
{
    [HtmlTargetElement("td", Attributes = "i-role")]
    public class RoleUsersTH : TagHelper
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleUsersTH(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HtmlAttributeName("i-role")]
        public string? Role { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            List<string> names = [];
            IdentityRole? role = await _roleManager.FindByIdAsync(Role);
            if (role != null)
            {
                var users = await _userManager.Users.AsNoTracking().ToListAsync();

                foreach (var user in users)
                {
                    if (user != null && await _userManager.IsInRoleAsync(user, role.Name ?? ""))
                        names.Add(user.UserName ?? "Usuário sem nome");
                }
            }
            output.Content.SetContent(names.Count == 0 ? "Nenhum usuário encontrado" : string.Join(", ", names));
        }
    }
}