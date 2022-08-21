using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Slackiffy.Installers.Interface;
using Slackiffy.Services.CacheService;
using Slackiffy.Services.Messages;
using Slackiffy.Services.Users;
using Slackiffy.Utilities;
using System.Linq;

namespace Slackiffy.Installers
{
    public class ServiceInstaller : IInstaller
    {
        public void InstallService(IServiceCollection service, IConfiguration Configuration)
        {
            service.AddScoped<CookiesProvider>();
            service.AddAutoMapper(typeof(SlackiffyProfile));
            service.AddSignalR(config => config.EnableDetailedErrors = true);
            service.AddResponseCompression(opt =>
            {
                opt.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
            });
            service.AddRazorPages();
            service.AddServerSideBlazor();
            service.AddMemoryCache();
            service.AddSingleton<ICacheService, CacheService>();
            service.AddScoped<IMessageService, MessageService>();
            service.AddScoped<IUserService, UserService>();

        }
    }
}
