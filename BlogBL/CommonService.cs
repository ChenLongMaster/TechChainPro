using BlogDALOld.Models;
using BlogDALOld.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogBLOld
{
    public class CommonService : ICommonService
    {
        private BlogContext _blogContext;
        public CommonService(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var entity = await _blogContext.Categories.ToListAsync();

            return entity;
        }

    }
}
