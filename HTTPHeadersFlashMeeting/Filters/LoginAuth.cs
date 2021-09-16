using Domain.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HTTPHeadersFlashMeeting.Filters
{
    public class LoginAuth : ActionFilterAttribute
    {
        private readonly IUserRepository _userRepository;

        public LoginAuth(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var password = context.HttpContext.Request.Headers["Password"];
            var username = context.HttpContext.Request.Headers["Username"];

            var user = _userRepository.Login(username, password);

            if (user is null)
                context.Result = new UnauthorizedResult();

            base.OnActionExecuting(context);
        }
    }
}
