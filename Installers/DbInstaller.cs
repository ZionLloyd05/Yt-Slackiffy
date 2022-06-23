using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Slackiffy.Data;
using Slackiffy.Installers.Interface;

namespace Slackiffy.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<SlackiffyDbContext>
                (opts => opts.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
