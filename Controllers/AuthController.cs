using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Slackiffy.Models;
using Slackiffy.Services.Users;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Slackiffy.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService userService;

        public AuthController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("google-login")]
        public async Task LoginAsync()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
            {
                RedirectUri = Url.Action(nameof(LoginCallBack))
            });
        }

        [HttpGet]
        public async Task<IActionResult> LoginCallBack()
        {
            var result = await HttpContext
                .AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            string email = result.Principal.FindFirst(ClaimTypes.Email).Value;

            var userInDb = await this.userService.GetUserByEmailAsync(email);

            if (userInDb == null)
            {
                await this.SaveUserDetails(result);
            }

            return Redirect("https://localhost:44374");
        }


        [HttpGet("signout")]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return Redirect("~/");
        }


        private async Task SaveUserDetails(AuthenticateResult result)
        {
            string email = result.Principal.FindFirst(ClaimTypes.Email).Value;
            string username = result.Principal.FindFirst(ClaimTypes.Name).Value;
            string picture = User.Claims.Where(c => c.Type == "picture").FirstOrDefault().Value;

            var user = new User
            {
                Username = username,
                Email = email,
                Picture = picture,
                DateJoined = DateTime.Now
            };

            await this.userService.RegisterUser(user);
        }
    }
}
