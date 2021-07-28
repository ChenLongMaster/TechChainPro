using BlogBL.Helpers;
using BlogDAL.Models;
using BlogDAL.Models.DTO;
using BlogDAL.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
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
        public async Task<IEnumerable<ArticleDTO>> GetArticles(ArticleFilter filter)
        {
            var entity = new List<ArticleDTO>();
            var query = from a in _blogContext.Articles
                        join u in _blogContext.Users on a.CreatedBy equals u.Id into x
                        from subUser in x.DefaultIfEmpty()
                        select new ArticleDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Abstract = a.Abstract,
                            DisplayContent = a.DisplayContent,
                            CategoryId = a.CategoryId,
                            RepresentImageUrl = a.RepresentImageUrl,
                            AuthorName = subUser.Username,
                            CreatedOn = a.CreatedOn
                        };

            if (filter.CategoryId != 0)
            {
                query = from a in _blogContext.Articles
                        join u in _blogContext.Users on a.CreatedBy equals u.Id into x
                        from subUser in x.DefaultIfEmpty()
                        where a.CategoryId == filter.CategoryId
                        select new ArticleDTO
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Abstract = a.Abstract,
                            DisplayContent = a.DisplayContent,
                            CategoryId = a.CategoryId,
                            RepresentImageUrl = a.RepresentImageUrl,
                            AuthorName = subUser.Username,
                            CreatedOn = a.CreatedOn
                        };
            }

            if (filter.SortDateDirection == (int)SortDirection.ASC)
            {
                entity = await query.OrderBy(x => x.CreatedOn).ToListAsync();
            }
            else
            {
                entity = await query.OrderByDescending(x => x.CreatedOn).ToListAsync();
            }

            #region use store procedure
            //var param = new SqlParameter("@CategoryId", filter.CategoryId);
            //var entity2 = _blogContext.Articles.FromSqlRaw("Exec GetAllArticles @CategoryId", param);
            //return entity;
            #endregion

            return entity;
        }
        public async Task<Article> GetArticleById(Guid Id)
        {
            var entity = await _blogContext.Articles.SingleAsync(x => x.Id == Id);

            return entity;
        }

        public async Task<Boolean> CreateArticle(ArticleDTO model)
        {
            var emptyId = Guid.Empty;
            var entity = new Article()
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                CategoryId = model.CategoryId,
                Abstract = model.Abstract,
                DisplayContent = model.DisplayContent,
                RepresentImageUrl = model.RepresentImageUrl,
                CreatedBy = emptyId,
                CreatedOn = DateTime.Now,
            };

            await _blogContext.Articles.AddAsync(entity);

            var result = await _blogContext.SaveChangesAsync();
            return result > 0;
        }
    }
}
