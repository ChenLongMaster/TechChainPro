using BlogDAL.Models;
using BlogDAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogBL
{

    public class UserService : IUserService
    {
        private readonly BlogContext _dbContext;
        public UserService(BlogContext blogContext)
        {
            _dbContext = blogContext;
        }

        public async Task<List<User>> GetAllUserAsync()
        {
            var result = await _dbContext.Users.ToListAsync();

            return result;
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            var entity = await _dbContext.Users.Include(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);

            return entity;
        }

        public async Task<User> GetUserByNameOrEmail(User user)
        {
            var entity = await _dbContext.Users.Include(x => x.Roles).FirstOrDefaultAsync(x => x.Username == user.Username && x.Provider == user.Provider);

            if(entity is null)
            {
                entity = await _dbContext.Users.Include(x => x.Roles).FirstOrDefaultAsync(x => x.Email == user.Email && x.Provider == user.Provider);
                if (entity is null)
                    return null;
            }

            return entity;
        }

        public async Task<IEnumerable<Role>> GetUserRolesById(Guid userId)
        {

            var entity = await _dbContext.Users.Include(x => x.Roles).FirstOrDefaultAsync(x => x.Id == userId);
            var result = entity.Roles;
            return result;
        }

        public async Task<bool> CreateUser(User user)
        {
            var salt = new byte[128 / 8];
            user.Salt = Convert.ToBase64String(salt);
            user.Roles = new Role[] { new() {Name = "Member"} };
            
            await _dbContext.Users.AddAsync(user);
            var result = await _dbContext.SaveChangesAsync();

            return result > 0;
        }

    }
}