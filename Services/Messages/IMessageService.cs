using Slackiffy.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Slackiffy.Services.Messages
{
    public interface IMessageService
    {
        Task SaveMessage(Message message);
        Task<ICollection<Message>> GetAllRecentConversations(int id);
        Task<ICollection<Message>> GetConversations(int fromId, int toId);
    }
}
