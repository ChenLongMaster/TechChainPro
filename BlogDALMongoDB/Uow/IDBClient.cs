using BlogDAL.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogDAL.Uow
{
    public interface IDBClient
    {
        IMongoCollection<Category> GetCategoryContext();
        IMongoCollection<User> GetUserContext();
        IMongoCollection<Role> GetRoleContext();
        IMongoCollection<Article> GetArticleContext();

    }
}
