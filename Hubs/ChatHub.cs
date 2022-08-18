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

                if (userEmail != null)
                {
                    var conkey = $"{userEmail}-{userName}";

                    conManager.Add(conkey, connectionId);
                }

                await Task.CompletedTask;
            }
            catch (Exception)
            {
                await Task.CompletedTask;
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

                conManager.Remove(conkey, connectionId);

                await Clients.All.SendAsync("UserDisconnected", userEmail);
            }

            await Task.CompletedTask;
        }

        private string GetConnectionId() => Context.ConnectionId;
    }
}
