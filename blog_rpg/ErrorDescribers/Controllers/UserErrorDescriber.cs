using Microsoft.Extensions.Localization;

namespace blog_rpg.ErrorDescribers.Controllers
{
    public class UserErrorDescriber : AppError
    {
        private readonly IStringLocalizer<UserErrorDescriber> _localizer;

        public UserErrorDescriber(IStringLocalizer<UserErrorDescriber> localizer)
        {
            _localizer = localizer;
        }

        public AppError UserNotFound()
        {
            return new AppError
            {
                Code = nameof(UserNotFound),
                Description = _localizer["UserNotFound"]
            };
        }

        public AppError PasswordHasUserName()
        {
            return new AppError
            {
                Code = nameof(PasswordHasUserName),
                Description = _localizer["PasswordHasUserName"]
            };
        }

        public AppError PasswordHas123Sequence()
        {
            return new AppError
            {
                Code = nameof(PasswordHas123Sequence),
                Description = _localizer["PasswordHas123Sequence"]
            };
        }
    }
}
