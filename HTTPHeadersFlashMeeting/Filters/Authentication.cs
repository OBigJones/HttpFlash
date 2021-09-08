using Domain.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HTTPHeadersFlashMeeting.Filters
{
    public class Authentication : ActionFilterAttribute
    {
        private readonly IUserRepository _userRepository;

        public Authentication(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var macHeader = context.HttpContext.Request.Headers["Adress"];
            var userName = context.HttpContext.Request.Headers["Name"];


            var user = _userRepository.GetUser(userName);

            if (!user.MacAdress.Equals(macHeader))
                context.Result = new UnauthorizedResult();

            base.OnActionExecuting(context);
        }
    }
}
