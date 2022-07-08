using Slackiffy.Models;
using Slackiffy.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Slackiffy.Services.Messages
{
    public interface IMessageService
    {
        ValueTask<Message> SaveMessage(Message message);
        ICollection<UsersForListingDTO>
            GetAllUsersWithPastConversation(string userEmail);
    }
}
