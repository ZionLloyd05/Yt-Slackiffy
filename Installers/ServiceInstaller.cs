using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Slackiffy.Installers.Interface;
using Slackiffy.Repository.Users;

namespace Slackiffy.Installers
{
    public class ServiceInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
