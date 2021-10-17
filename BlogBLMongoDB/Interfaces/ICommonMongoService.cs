using TechchainDAL.Models;
using System.Collections.Generic;

namespace TechchainBL.Interfaces
{
    public interface ICommonMongoService
    {
        List<Category> GetCategories();

        bool CreateCategory(Category model);
    }
}