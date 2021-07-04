using BlogDAL.Models;
using BlogDAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogBL
{

    public class UserService : IUserService
    {
        private readonly BlogContext _blogContext;
        public UserService(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public async Task<List<User>> GetAllUserAsync()
        {
            var result = await _blogContext.Users.ToListAsync();

            return result;
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            var entity = await _blogContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            return entity;
        }
    }
}