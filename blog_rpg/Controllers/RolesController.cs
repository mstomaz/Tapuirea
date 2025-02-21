using blog_rpg.Areas.Identity.Models;
using blog_rpg.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace blog_rpg.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index() => View(_roleManager.Roles.ToList());

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([Required] string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                    Errors(result);
            }
            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole? role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                    Errors(result);
            }
            else
                ModelState.AddModelError("", "Permissão não encontrada");
            return View(nameof(Index), _roleManager.Roles.ToList());
        }

        public async Task<IActionResult> Update(string id)
        {
            IdentityRole? role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                List<ApplicationUser> members = [];
                List<ApplicationUser> nonMembers = [];
                foreach (ApplicationUser? user in _userManager.Users.ToList())
                {
                    var list = await _userManager.IsInRoleAsync(user, role.Name!) ? members : nonMembers;
                    list.Add(user);
                }
                return View(new RoleEdit
                {
                    Role = role,
                    Members = members,
                    NonMembers = nonMembers
                });
            }
            else
                ModelState.AddModelError("", "Permissão não encontrada");

            return View(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Update(RoleModification model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.AddIds ?? [])
                {
                    ApplicationUser? user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await _userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            Errors(result);
                    }
                }
                foreach (string userId in model.DeleteIds ?? [])
                {
                    ApplicationUser? user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            Errors(result);
                    }
                }
            }

            if (ModelState.IsValid)
                return RedirectToAction(nameof(Index));
            else
                return await Update(model.RoleId);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
