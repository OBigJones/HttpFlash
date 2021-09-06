using Domain.User;
using Domain.User.Model;
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
        public Task CreateUser(User user)
        {
            return _userService.CreateUser(user);
        }

        [HttpPost("POST_SIM")]
        public Task<User> GetUser(string name)
        {
            return _userService.GetUser(name);
        }
    }
}
