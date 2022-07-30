using Microsoft.EntityFrameworkCore;
using Slackiffy.Data;
using Slackiffy.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slackiffy.Services.Users
{
    public class UserService : IUserService
    {
        private readonly SlackiffyDbContext context;

        public UserService(SlackiffyDbContext context)
        {
            this.context = context;
        }

        public ValueTask<ICollection<User>> GetAllUsers()
        {
            throw new System.NotImplementedException();
        }

        public async ValueTask<User> GetUserByEmail(string email)
        {
            var userInDb = await this.context.Users
               .Where(user => user.Email == email)
               .SingleOrDefaultAsync();

            return userInDb;
        }

        public async ValueTask<User> GetUserById(int Id)
        {
            var userInDb = await this.context.Users
                .Where(user => user.Id == Id)
                .SingleOrDefaultAsync();

            return userInDb;
        }

        public async ValueTask<User> RegisterUser(User user)
        {
            this.context.Users.Add(user);
            await this.context.SaveChangesAsync();

            return user;
        }
    }
}
