using Microsoft.EntityFrameworkCore;
using Slackiffy.Data;
using Slackiffy.Models;
using Slackiffy.Services.CacheService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slackiffy.Services.Users
{
    public class UserService : IUserService
    {
        private readonly SlackiffyDbContext context;
        private readonly ICacheService cacheService;
        private readonly object lockObject = new object();

        public UserService(SlackiffyDbContext context, ICacheService cacheService)
        {
            this.context = context;
            this.cacheService = cacheService;
        }

        public Task<ICollection<User>> GetAllUsers()
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var userInCache = this.cacheService.GetData<User>(email);
            if (userInCache != null)
            {
                return userInCache;
            }

            var expirationTime = DateTimeOffset.Now.AddMinutes(30);

            var userInDb = await this.context.Users
               .Where(user => user.Email == email)
               .AsNoTracking()
               .SingleOrDefaultAsync();

            this.cacheService.SetData(email, userInDb, expirationTime);
            return userInDb;
        }

        public async Task<User> GetUserById(int Id)
        {
            var userInDb = await this.context.Users
                .Where(user => user.Id == Id)
                .SingleOrDefaultAsync();

            return userInDb;
        }

        public async Task<User> RegisterUser(User user)
        {
            this.context.Users.Add(user);
            await this.context.SaveChangesAsync();

            return user;
        }
    }
}
