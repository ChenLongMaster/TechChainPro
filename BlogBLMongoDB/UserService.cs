using BlogBL.Interfaces;
using BlogDAL.Models;
using BlogDAL.Uow;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BlogBL
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _users;
        private readonly IMongoCollection<Role> _roles;

        public UserService(IDBClient dbClient)
        {
            _users = dbClient.GetUserContext();
            _roles = dbClient.GetRoleContext();
        }

        public async Task<List<User>> GetAllUserAsync()
        {
            var result = await _users.FindSync(x => true).ToListAsync();

            return result;
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            var entity = await _users.FindSync(x => x.Id == id).FirstOrDefaultAsync();

            return entity;
        }

        public async Task<User> GetUserByNameOrEmail(User user)
        {
            var entity = await _users.FindSync(x => x.Username == user.Username && x.Provider == user.Provider).FirstOrDefaultAsync();

            if (entity is null)
            {
                entity = await _users.FindSync(x => x.Email == user.Email && x.Provider == user.Provider).FirstOrDefaultAsync();
                if (entity is null)
                    return null;
            }
            return entity;
        }

        public async Task<bool> CreateUser(User user)
        {
            var random = new RNGCryptoServiceProvider();
            user.Salt = Guid.NewGuid().ToString();

            var memberRole = await GetRoleByName("Member");
            user.Roles = new string[] { memberRole.Id };

            await _users.InsertOneAsync(user);

            return true;
        }

        public async Task<Role> GetRoleByName(string name)
        {
            var result = await _roles.FindSync(x => x.Name == name).FirstOrDefaultAsync();
            return result;
        }

        public async Task<Role> GetRoleById(string iId)
        {
            var result = await _roles.FindSync(x => x.Id == iId).FirstOrDefaultAsync();
            return result;
        }
    }
}
