using TechchainBL.Interfaces;
using TechchainDAL.Models;
using TechchainDAL.Uow;
using MongoDB.Driver;
using System.Collections.Generic;

namespace TechchainBL
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
