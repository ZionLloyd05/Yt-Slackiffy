using Slackiffy.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Slackiffy.Services.Users
{
    public interface IUserService
    {
        ValueTask<User> RegisterUser(User user);
        ValueTask<User> GetUserById(int Id);
        ValueTask<User> GetUserByEmail(string email);
        ValueTask<ICollection<User>> GetAllUsers();
    }
}
