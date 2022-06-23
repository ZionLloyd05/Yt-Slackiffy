using Microsoft.EntityFrameworkCore;
using Slackiffy.Models;

namespace Slackiffy.Data
{
    public class SlackiffyDbContext : DbContext
    {
        public SlackiffyDbContext(DbContextOptions<SlackiffyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }
}
