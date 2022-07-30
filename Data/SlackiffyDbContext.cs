using Microsoft.EntityFrameworkCore;
using Slackiffy.Models;

namespace Slackiffy.Data
{
    public class SlackiffyDbContext : DbContext
    {
        public SlackiffyDbContext(DbContextOptions options)
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
                entity.HasOne(message => message.FromUser)
                    .WithMany(message => message.ChatMessagesFromUsers)
                    .HasForeignKey(message => message.FromUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(message => message.ToUser)
                    .WithMany(message => message.ChatMessagesToUsers)
                    .HasForeignKey(message => message.ToUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
    }
}
