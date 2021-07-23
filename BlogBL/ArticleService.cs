using BlogDAL.Models;
using BlogDAL.Models.DTO;
using BlogDAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogBL
{
    public class ArticleService : IArticleService
    {
        private BlogContext _blogContext;
        public ArticleService(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }
        public async Task<Article> GetArticles(ArticleFilter filter)
        {
            var entity = await _blogContext.Articles.SingleAsync(x => x.Name == filter.Name);

            return entity;
        }
        public async Task<Article> GetArticleById(Guid Id)
        {
            var entity = await _blogContext.Articles.SingleAsync(x => x.Id == Id);

            return entity;
        }

        public async Task<Boolean> CreateArticle(Article model)
        {

            model.CreatedOn = DateTime.Now;

            await _blogContext.Articles.AddAsync(model);

            var result = await _blogContext.SaveChangesAsync();
            return result > 0;
        }
    }
}
