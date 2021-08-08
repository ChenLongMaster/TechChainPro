using BlogDAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogBL
{
    public interface IUserService
    {
        Task<List<User>> GetAllUserAsync();
        Task<User> GetUserByIdAsync(Guid id);
        Task<User> GetUserByNameOrEmail(User user);
        Task<IEnumerable<Role>> GetUserRolesById(Guid userId);
        Task<bool> CreateUser(User user);

    }
}