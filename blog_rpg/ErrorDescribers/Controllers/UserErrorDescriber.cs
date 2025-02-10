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
    }
}
