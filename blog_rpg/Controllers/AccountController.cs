using blog_rpg.Areas.Identity.Models;
using blog_rpg.ErrorDescribers.Controllers;
using blog_rpg.Models;
using blog_rpg.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace blog_rpg.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly IStringLocalizer<UserErrorDescriber> _userErrorLocalizer;

        public AccountController(UserManager<ApplicationUser> userManager, IPasswordHasher<ApplicationUser> passwordHasher,
            IStringLocalizer<UserErrorDescriber> userErrorLocalizer)
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _userErrorLocalizer = userErrorLocalizer;
        }

        public IActionResult Index()
        {
            return View(_userManager.Users);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel user)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appUser = new()
                {
                    UserName = user.Name,
                    Email = user.Email
                };

                IdentityResult result = await _userManager.CreateAsync(appUser, user.Password);

                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                {
                    Errors(result);
                }
            }
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                var viewModel = new UpdateUserViewModel
                {
                    Id = user.Id,
                    Email = user.Email
                };

                return View(viewModel);
            }
            else
                return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateUserViewModel viewModel)
        {
            if (viewModel == null)
            {
                ModelState.AddModelError("", _userErrorLocalizer["UserNotFound"]);
            }
            else
            {
                var user = await _userManager.FindByIdAsync(viewModel.Id!);

                if (!string.IsNullOrEmpty(viewModel.Email))
                {
                    user!.Email = viewModel.Email;

                    IdentityResult result = await _userManager.UpdateAsync(user!);
                    if (result.Succeeded)
                        return RedirectToAction(nameof(Index));
                    else
                        Errors(result);
                }
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            ApplicationUser? user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                    Errors(result);
            }
            else
                ModelState.AddModelError("", _userErrorLocalizer["UserNotFound"]);
            return View(nameof(Index), _userManager.Users);
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
    }
}
