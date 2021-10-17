using TechchainDAL.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace TechchainDAL.Uow
{
    public class DBClient : IDBClient
    {
        private readonly IMongoCollection<Category> _categories;
        private readonly IMongoCollection<User> _users;
        private readonly IMongoCollection<Role> _roles;
        private readonly IMongoCollection<Article> _articles;

        public DBClient(IOptions<DBConfig> dbConfig)
        {
            var client = new MongoClient(dbConfig.Value.Connection_String);
            var database = client.GetDatabase(dbConfig.Value.Database_Name);

            _categories = database.GetCollection<Category>("Category");
            _users = database.GetCollection<User>("User");
            _roles = database.GetCollection<Role>("Role");
            _articles = database.GetCollection<Article>("Article");
        }
        public IMongoCollection<Category> GetCategoryContext() => _categories;
        public IMongoCollection<User> GetUserContext() => _users;
        public IMongoCollection<Role> GetRoleContext() => _roles;
        public IMongoCollection<Article> GetArticleContext() => _articles;
    }
}
