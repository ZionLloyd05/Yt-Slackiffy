using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Slackiffy.Installers.Interface;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Slackiffy.Installers
{
    public class AuthSchemeInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
            .AddCookie(option =>
            {
                option.Cookie.Name = "SlackiffyAuthCookie";
                option.LoginPath = "/auth/google-login";
            })
            .AddGoogle(options =>
            {
                IConfigurationSection googleAuthNSection =
                    Configuration.GetSection("Authentication:Google");

                options.ClientId = googleAuthNSection["ClientId"];
                options.ClientSecret = googleAuthNSection["ClientSecret"];
                options.Scope.Add("Profile");
                options.Events.OnCreatingTicket = context =>
                {
                    string pictureUri = context.User.GetProperty("picture").GetString();
                    context.Identity.AddClaim(new Claim("picture", pictureUri));
                    return Task.CompletedTask;
                };
            });
        }
    }
}
