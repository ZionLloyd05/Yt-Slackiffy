using Slackiffy.Components.Chat;
using System.Threading.Tasks;

namespace Slackiffy.Repository.Messages
{
    public interface IMessageRepository
    {
        ValueTask<Message> SaveMessage(Message message);
    }
}
