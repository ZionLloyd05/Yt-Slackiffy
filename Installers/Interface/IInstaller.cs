using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Slackiffy.Installers.Interface
{
    public interface IInstaller
    {
        void InstallServices(IServiceCollection services, IConfiguration Configuration);
    }
}
