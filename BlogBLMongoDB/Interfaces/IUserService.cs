using TechchainDAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TechchainBL.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateUser(User user);
        Task<List<User>> GetAllUserAsync();
        Task<Role> GetRoleByName(string name);
        Task<User> GetUserByIdAsync(string id);
        Task<User> GetUserByNameOrEmail(User user);
    }
}