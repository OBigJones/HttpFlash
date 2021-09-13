using Domain.User;
using Domain.User.Model;
using HTTPHeadersFlashMeeting.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HTTPHeadersFlashMeeting.Controllers
{
    [Route("v{api-version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private IConfiguration _config;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _config = configuration;
        }

        [HttpPost("CreateAccount")]
        public Task CreateUser(User user)
        {
            return _userService.CreateUser(user);
        }

        [Authorize]
        [HttpGet("ValidateSession")]
        public IActionResult ValidateSession()
        {
            var jwt = this.HttpContext.Request.Headers["Authorization"];
            var justJWT = jwt.ToString().Split(" ")[1];
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(justJWT);
            List<Claim> jti = (List<Claim>)jwtSecurityToken.Claims;
            var exp = int.Parse(jti[0].Value);
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(exp);
            var result = Ok("Sua sessão expira em " + dateTimeOffset);
            return result; 
        }

        [HttpPost("Login")]
        [ServiceFilter(typeof(LoginAuth))]
        public IActionResult Login()
        {
            if (!HttpContext.Equals(Unauthorized()))
            {
                var tokenString = GerarTokenJWT();
                return Ok(new { token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }


        private string GerarTokenJWT()
        {
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var expiry = DateTime.Now.AddMinutes(120);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: issuer, audience: audience,
            expires: DateTime.Now.AddMinutes(120), signingCredentials: credentials);
            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
    }
}
