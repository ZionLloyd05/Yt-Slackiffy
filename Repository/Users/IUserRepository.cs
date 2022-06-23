using Slackiffy.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Slackiffy.Repository.Users
{
    public interface IUserRepository
    {
        ValueTask<User> RegisterUser(User user);

        ValueTask<User> GetUserById(int Id);

        ValueTask<ICollection<User>> GetUsers();

        ValueTask<User> GetUserByEmail(string email);
    }
}
