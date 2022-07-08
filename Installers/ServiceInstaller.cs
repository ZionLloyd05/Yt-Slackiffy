using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Slackiffy.Data;
using Slackiffy.Installers.Interface;
using Slackiffy.Services.Messages;
using Slackiffy.Services.Users;
using System.Linq;

namespace Slackiffy.Installers
{
    public class ServiceInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<CookiesProvider>();
            services.AddSignalR(o => {
                o.EnableDetailedErrors = true;
            });
            /// helps to compress data passed between client & server
            services.AddResponseCompression(opt =>
            {
                /// helps server know the data type of what's passed
                opt.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
            });

            services.AddSingleton<ConnectionManager>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMessageService, MessageService>();
        }
    }
}
