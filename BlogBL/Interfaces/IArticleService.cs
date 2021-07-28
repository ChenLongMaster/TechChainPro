using BlogDAL.Models;
using BlogDAL.Models.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogBL
{
    public interface IArticleService
    {
        Task<Article> GetArticleById(Guid Id);
        Task<IEnumerable<ArticleDTO>> GetArticles(ArticleFilter filter);
        Task<Boolean> CreateArticle(ArticleDTO model);
    }
}