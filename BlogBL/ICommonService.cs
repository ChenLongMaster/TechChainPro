using BlogDAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogBL
{
    public interface ICommonService
    {
        Task<IEnumerable<Category>> GetCategories();
    }
}