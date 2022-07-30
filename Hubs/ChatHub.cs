using Microsoft.AspNetCore.Authorization;
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
        private readonly ConnectionManager conManager;
        private readonly IUserService userService;

        public ChatHub(ConnectionManager conManager, IUserService userService)
        {
            this.conManager = conManager;
            this.userService = userService;
        }

        public async Task InitializeUserList()
        {
            var list = this.conManager.GetUsers();

            await Clients.All.SendAsync("ReceiveInitializeUserList", list);
        }

        public void AddUserToRoom()
        {
            var currentUser = Context.User.Claims
                .Where(claim => claim.Type == ClaimTypes.Email).FirstOrDefault().Value;

            var connectionId = GetConnectionId();
            this.conManager.Add(currentUser, connectionId);
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

                if (userEmail != null)
                {
                    var conkey = $"{userEmail}-{userName}";

                    this.conManager.Add(conkey, connectionId);
                }
                    
                await base.OnConnectedAsync();
            }
            catch (Exception)
            {
                await base.OnConnectedAsync();
            }
           
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userEmail = Context.User.Claims
                .Where(claim => claim.Type == ClaimTypes.Email).FirstOrDefault()?.Value;
            var userName = Context.User.Claims
                .Where(claim => claim.Type == ClaimTypes.Name).FirstOrDefault()?.Value;

            if(userEmail != null)
            {
                var conkey = $"{userEmail}-{userName}";

                var connectionId = GetConnectionId();

                this.conManager.Remove(conkey, connectionId);

                await Clients.All.SendAsync("UserDisconnected", userEmail);
            }
            
            await base.OnDisconnectedAsync(exception);
        }

        private string GetConnectionId() => Context.ConnectionId;
    }
}
