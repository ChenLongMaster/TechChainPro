using BlogBL.Interfaces;
using BlogDAL.Models;
using BlogDAL.Uow;
using MongoDB.Driver;
using System.Collections.Generic;

namespace BlogBL
{
    public class CommonMongoService : ICommonMongoService
    {
        private readonly IMongoCollection<Category> _categories;

        public CommonMongoService(IDBClient dbClient)
        {
            _categories = dbClient.GetCategoryContext();
        }

        public bool CreateCategory(Category model)
        {
            _categories.InsertOne(model);
            return true;
        }

        public List<Category> GetCategories()
        {
            var result = _categories.Find(x => true).ToList();
            return result;
        }


    }
}
