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
            builder.Entity<Message>(entity =>
            {
                entity.HasOne(d => d.FromUser)
                    .WithMany(p => p.ChatMessagesFromUsers)
                    .HasForeignKey(d => d.FromUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(d => d.ToUser)
                    .WithMany(p => p.ChatMessagesToUsers)
                    .HasForeignKey(d => d.ToUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
    }
}
