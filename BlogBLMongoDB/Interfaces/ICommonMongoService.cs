using BlogDAL.Models;
using System.Collections.Generic;

namespace BlogBL.Interfaces
{
    public interface ICommonMongoService
    {
        List<Category> GetCategories();

        bool CreateCategory(Category model);
    }
}