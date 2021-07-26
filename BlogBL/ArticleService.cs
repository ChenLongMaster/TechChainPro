using BlogDAL.Models;
using BlogDAL.Models.DTO;
using BlogDAL.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
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

        public async Task<Boolean> CreateArticle(ArticleDTO model)
        {

            var entity = new Article()
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Category = model.Category,
                Abstract = model.Abstract,
                DisplayContent = model.DisplayContent,
                CreatedBy = model.CreatedBy,
                CreatedOn = DateTime.Now,
            };

            await _blogContext.Articles.AddAsync(entity);

            var result = await _blogContext.SaveChangesAsync();
            return result > 0;
        }
    }
}
