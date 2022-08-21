using Slackiffy.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Slackiffy.Services.Users
{
    public interface IUserService
    {
        Task<User> RegisterUser(User user);
        Task<User> GetUserById(int Id);
        Task<User> GetUserByEmailAsync(string email);
        Task<ICollection<User>> GetAllUsers();
    }
}
