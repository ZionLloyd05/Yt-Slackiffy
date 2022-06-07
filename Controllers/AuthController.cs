using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Slackiffy.APIs
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Home() =>
            RedirectToPage("Index");

        [HttpGet("google-login")]
        public async Task LoginAsync()
        {
            await HttpContext.ChallengeAsync
                (GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
            {
             RedirectUri = "/"
            });
        }

        [HttpGet("signout")]
        public async Task<IActionResult> LogOutAsync()
        {
            await HttpContext.SignOutAsync();
            return Redirect("~/");

        }
    }
}
