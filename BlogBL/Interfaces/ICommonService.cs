using BlogDALOld.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogBLOld
{
    public interface ICommonService
    {
        Task<IEnumerable<Category>> GetCategories();
    }
}