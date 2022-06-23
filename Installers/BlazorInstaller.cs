using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Slackiffy.Installers.Interface;

namespace Slackiffy.Installers
{
    public class BlazorInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
        }
    }
}
