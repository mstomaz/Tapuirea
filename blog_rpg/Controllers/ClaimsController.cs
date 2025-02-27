using blog_rpg.Areas.Identity.Models;
using blog_rpg.ErrorDescribers.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace blog_rpg.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class ClaimsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IStringLocalizer<UserErrorDescriber> _userErrorLocalizer;

        public ClaimsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IStringLocalizer<UserErrorDescriber> userErrorLocalizer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userErrorLocalizer = userErrorLocalizer;
        }

        public async Task<IActionResult> Index()
        {
            ApplicationUser? user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                ModelState.AddModelError("", _userErrorLocalizer["UserNotFound"]);
            }
            else
            {
                await _signInManager.RefreshSignInAsync(user);
            }

            return View(User?.Claims);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(string claimType, string claimValue)
        {
            ApplicationUser? user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                ModelState.AddModelError("", _userErrorLocalizer["UserNotFound"]);
            }
            else
            {
                Claim claim = new(claimType, claimValue, ClaimValueTypes.String);
                IdentityResult result = await _userManager.AddClaimAsync(user, claim);

                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                    Errors(result);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string claimValues)
        {
            ApplicationUser? user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                ModelState.AddModelError("", _userErrorLocalizer["UserNotFound"]);
            }
            else
            {
                string[] claimValuesArray = claimValues.Split(";");
                string claimType = claimValuesArray[0], claimValue = claimValuesArray[1], claimIssuer = claimValuesArray[2];

                Claim? claim = User.Claims.Where(x => x.Type == claimType && x.Value == claimValue && x.Issuer == claimIssuer).FirstOrDefault();

                IdentityResult result = await _userManager.RemoveClaimAsync(user, claim);

                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                    Errors(result);
            }

            return View(nameof(Index));
        }

        void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
    }
}
