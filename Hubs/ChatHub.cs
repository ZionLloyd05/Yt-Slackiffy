using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR;
using Slackiffy.Data;
using Slackiffy.Models;
using Slackiffy.Models.DTO;
using Slackiffy.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Slackiffy.Hubs
{
    [Authorize()]
    public class ChatHub : Hub
    {
        private readonly ConnectionManager connManager;
        private readonly IUserService userService;

        public ChatHub(ConnectionManager connManager, IUserService userService)
        {
            this.connManager = connManager;
            this.userService = userService;
        }

        public async Task InitializeUserList()
        {
            var list = this.connManager.GetUsers();

            await Clients.All.SendAsync("ReceiveInitializeUserList", list);
        }

        public async Task SendMessageToClient(string message, int fromId, int toId, string recieverId)
        {
            await Clients.Client(recieverId).SendAsync("RecieveMessage", message);
        }

        public void AddUserToRoom(string userEmail)
        {
            var currentUser = Context.User.Claims
                .Where(claim => claim.Type == ClaimTypes.Email).FirstOrDefault().Value;

            var connectionId = GetConnectionId();

            this.connManager.Add(currentUser, connectionId);
        }

        public override async Task OnConnectedAsync()
        {            
            var connectionId = GetConnectionId();

            var userEmail = Context.User.Claims
                .Where(claim => claim.Type == ClaimTypes.Email).FirstOrDefault().Value;
            var userName = Context.User.Claims
                .Where(claim => claim.Type == ClaimTypes.Name).FirstOrDefault().Value;

            var conKey = $"{userEmail}-{userName}";

            var connections = this.connManager.GetConnections(conKey);

            //if (!connections.Any())
            //{
                
            //    var userInDb = await this.userService.GetUserByEmail(userEmail);
            //    UsersForListingDTO newUser = new()
            //    {
            //        Email = userInDb.Email,
            //        Picture = userInDb.Picture,
            //        Username = userInDb.Username
            //    };

            //    //await Clients.All.SendAsync("JoinedUser", newUser);
            //}

            this.connManager.Add(conKey, connectionId);
            await base.OnConnectedAsync();
        }


        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userEmail = Context.User.Claims
                .Where(claim => claim.Type == ClaimTypes.Email).FirstOrDefault().Value;
            var userName = Context.User.Claims
                .Where(claim => claim.Type == ClaimTypes.Name).FirstOrDefault().Value;

            var conKey = $"{userEmail}-{userName}";

            var connectionId = GetConnectionId();

            this.connManager.Remove(conKey, connectionId);

            await Clients.All.SendAsync("UserDisconnected", userEmail);
            await base.OnDisconnectedAsync(exception);
        }

        public string GetConnectionId() => Context.ConnectionId;
    }
}
