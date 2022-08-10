using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Slackiffy.Models;
using Slackiffy.Services.Users;
using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Slackiffy.APIs
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService repository;

        public AuthController(IUserService repository)
        {
            this.repository = repository;
        }

        [HttpGet("google-login")]
        public async Task LoginAsync()
        {
            await HttpContext
            .ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
            {
                RedirectUri = Url.Action(nameof(LoginCallback))
            });
        }

        [HttpGet]
        public async Task<IActionResult> LoginCallback()
        {
            var result = await HttpContext
                .AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            string email = result.Principal.FindFirst(ClaimTypes.Email).Value;

            var userInDb = await this.repository
                .GetUserByEmail(email);

            if(userInDb == null)
            {
                await this.SaveUserDetails(result);
            }

            return Redirect("https://localhost:44374/");
        }

        [HttpGet("signout")]
        public async Task<IActionResult> LogOutAsync()
        {
            await HttpContext.SignOutAsync();
            return Redirect("~/");

        }

        private async Task SaveUserDetails(AuthenticateResult result)
        {
            string email = result.Principal.FindFirst(ClaimTypes.Email).Value;
            string username = result.Principal.FindFirst(ClaimTypes.Name).Value;
            string userNameId = result.Principal.FindFirst(ClaimTypes.NameIdentifier).Value;
            string picture = User.Claims.Where(c => c.Type == "picture").FirstOrDefault().Value;

            var user = new User
            {
                Username = username,
                Email = email,
                Picture = picture,
                UserNameId = userNameId,
                DateJoined = DateTime.Now
            };

            var savedUser = await this.repository.RegisterUser(user);
        }
    }
}
