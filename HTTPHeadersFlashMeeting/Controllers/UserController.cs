using Domain.User;
using Domain.User.Model;
using HTTPHeadersFlashMeeting.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HTTPHeadersFlashMeeting.Controllers
{
    [Route("v{api-version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [ServiceFilter(typeof(Authentication))]
        public Task CreateUser(User user)
        {
            return _userService.CreateUser(user);
        }
    }
}
