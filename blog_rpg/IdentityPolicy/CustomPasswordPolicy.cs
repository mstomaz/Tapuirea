using Microsoft.AspNetCore.Identity;
using blog_rpg.Areas.Identity.Models;
using Microsoft.Extensions.Localization;
using blog_rpg.ErrorDescribers.Controllers;

namespace blog_rpg.IdentityPolicy
{
    public class CustomPasswordPolicy : PasswordValidator<ApplicationUser>
    {
        private readonly IStringLocalizer<UserErrorDescriber> _userErrorLocalizer;

        public CustomPasswordPolicy(IStringLocalizer<UserErrorDescriber> userErrorLocalizer)
        {
            _userErrorLocalizer = userErrorLocalizer;
        }

        public override Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user, string? password)
        {
            List<IdentityError>? errors = null;

            if (password!.Contains(user.UserName!, StringComparison.CurrentCultureIgnoreCase))
            {
                errors ??= [];
                errors.Add(new IdentityError
                {
                    Description = _userErrorLocalizer["PasswordHasUserName"]
                });
            }
            if (password.Contains("123"))
            {
                errors ??= [];
                errors.Add(new IdentityError
                {
                    Description = _userErrorLocalizer["PasswordHas123Sequence"]
                });
            }
            return Task.FromResult(errors?.Count > 0
                ? IdentityResult.Failed([.. errors])
                : IdentityResult.Success);
        }
    }
}
