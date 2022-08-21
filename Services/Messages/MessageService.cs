using Microsoft.EntityFrameworkCore;
using Slackiffy.Data;
using Slackiffy.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slackiffy.Services.Messages
{
    public class MessageService : IMessageService
    {
        private readonly SlackiffyDbContext context;

        public MessageService(SlackiffyDbContext context)
        {
            this.context = context;
        }
        public async Task<ICollection<Message>> GetAllRecentConversations(int id)
        {
            return await this.context.Messages
                .Where(msg => msg.FromUserId == id || msg.ToUserId == id)
                .Include(msg => msg.FromUser)
                .OrderByDescending(msg => msg.CreatedDate)
                .Take(20)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ICollection<Message>> GetConversations(int fromId, int toId)
        {
            return await this.context.Messages
                .Where(msg => (msg.FromUserId == fromId && msg.ToUserId == toId)
                    || (msg.FromUserId == toId && msg.ToUserId == fromId))
                .Include(msg => msg.FromUser)
                .OrderByDescending(msg => msg.CreatedDate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task SaveMessage(Message message)
        {
            context.Messages.Add(message);
            await context.SaveChangesAsync();
        }
    }
}
