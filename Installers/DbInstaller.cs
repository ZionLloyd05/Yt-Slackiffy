using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Slackiffy.Data;
using Slackiffy.Installers.Interface;

namespace Slackiffy.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallService(IServiceCollection service, IConfiguration Configuration)
        {
            service.AddDbContext<SlackiffyDbContext>
                (opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
