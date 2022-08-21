using Microsoft.AspNetCore.SignalR;
using Slackiffy.Data;
using Slackiffy.Services.Users;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Slackiffy.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IUserService userService;
        private readonly static ConnectionManager<string> conManager = new ConnectionManager<string>();

        public ChatHub(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task InitializeUserList()
        {
            var list = conManager.GetUsers();

            await Clients.All.SendAsync("ReceiveInitializeUserList", list);
        }

        public void AddUserToRoom()
        {
            var currentUser = Context.User.Claims
                .Where(claim => claim.Type == ClaimTypes.Email).FirstOrDefault().Value;

            var connectionId = GetConnectionId();
            conManager.Add(currentUser, connectionId);
        }

        public override async Task OnConnectedAsync()
        {
            var connectionId = GetConnectionId();

            try
            {
                var userEmail = Context.User.Claims
               .Where(claim => claim.Type == ClaimTypes.Email).FirstOrDefault().Value;
                var userName = Context.User.Claims
                    .Where(claim => claim.Type == ClaimTypes.Name).FirstOrDefault().Value;

                var userInDb = await this.userService.GetUserByEmailAsync(userEmail);

                if (userEmail != null)
                {
                    var conkey = $"{userEmail}-{userName}-{userInDb.Id}";

                    conManager.Add(conkey, connectionId);
                }

                await Task.CompletedTask;
            }
            catch (Exception)
            {
                await Task.CompletedTask;
            }

        }

        public async Task SendMessage(string recieverEmail, string message)
        {
            var userName = Context.User.Claims
                .Where(claim => claim.Type == ClaimTypes.Name).FirstOrDefault()?.Value;
            var userEmail = Context.User.Claims
                .Where(claim => claim.Type == ClaimTypes.Email).FirstOrDefault()?.Value;
            var userPics = Context.User.Claims
               .Where(claim => claim.Type == "picture").FirstOrDefault()?.Value;

            var msgSender = await this.userService.GetUserByEmailAsync(userEmail);
            var senderKey = $"{userEmail}-{userName}-{msgSender.Id}";
            var msgReciever = await this.userService.GetUserByEmailAsync(recieverEmail);
            var recieverkey = $"{msgReciever.Email}-{msgReciever.Username}-{msgReciever.Id}";

            var msgPack = new Models.DTOs.MessagePack()
            {
                UserName = userName,
                Message = message,
                Picture = userPics,
                CreatedAt = DateTime.Now,
            };
                       
            var recieverConnectionIds = conManager.GetConnections(recieverkey);
            var senderConnectionIds = conManager.GetConnections(senderKey);

            if (recieverkey == senderKey)
            {

                foreach (var connId in recieverConnectionIds)
                {
                    await Clients.Client(connId).SendAsync("RecievePrivateMessage", msgPack);
                }
            }
            else
            {

                foreach (var connId in recieverConnectionIds)
                {
                    await Clients.Client(connId).SendAsync("RecievePrivateMessage", msgPack);
                }

                foreach (var connId in senderConnectionIds)
                {
                    await Clients.Client(connId).SendAsync("RecievePrivateMessage", msgPack);
                }
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userEmail = Context.User.Claims
                .Where(claim => claim.Type == ClaimTypes.Email).FirstOrDefault()?.Value;
            var userName = Context.User.Claims
                .Where(claim => claim.Type == ClaimTypes.Name).FirstOrDefault()?.Value;
            
            var user = await this.userService.GetUserByEmailAsync(userEmail);

            if (userEmail != null)
            {
                var conkey = $"{userEmail}-{userName}-{user.Id}";

                var connectionId = GetConnectionId();

                conManager.Remove(conkey, connectionId);

                await Clients.All.SendAsync("UserDisconnected", userEmail);
            }

            await Task.CompletedTask;
        }

        private string GetConnectionId() => Context.ConnectionId;
    }
}
