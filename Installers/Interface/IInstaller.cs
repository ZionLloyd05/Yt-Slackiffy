using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Slackiffy.Installers.Interface
{
    public interface IInstaller
    {
        void InstallService(IServiceCollection service, IConfiguration Configuration);
    }
}
