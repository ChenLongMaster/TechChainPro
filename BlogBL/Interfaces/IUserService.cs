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
    }
}