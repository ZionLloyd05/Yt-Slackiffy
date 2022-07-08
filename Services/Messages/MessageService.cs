using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Slackiffy.Data;
using Slackiffy.Models;
using Slackiffy.Models.DTO;
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

        public ICollection<UsersForListingDTO> GetAllUsersWithPastConversation(string userEmail)
        {
            var allUsersWithConversation = new List<UsersForListingDTO>();

            lock (this.context)
            {
                 allUsersWithConversation = this.context.Messages
                .Include(msg => msg.FromUser)
                .Where(msg => msg.FromUser.Email == userEmail)
                .Include(msg => msg.ToUser)
                .Select(user => new UsersForListingDTO
                {
                    Email = user.ToUser.Email,
                    Username = user.ToUser.Username,
                    Picture = user.ToUser.Picture
                })
                .ToList();
            }
            

            return allUsersWithConversation;
        }

        public ValueTask<Message> SaveMessage(Message message)
        {
            throw new System.NotImplementedException();
        }
    }
}
