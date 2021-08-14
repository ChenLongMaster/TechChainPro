using BlogDAL.Models;
using BlogDAL.Models.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogBL
{
    public interface IArticleService
    {
        Task<bool> CreateArticle(ArticleDTO model);
        Task<bool> UpdateArticle(ArticleDTO model);

        Task<IEnumerable<ArticleDTO>> GetArticles(ArticleFilter filter);
        Task<IEnumerable<ArticleDTO>> GetRecommendedArticles();
        Task<Article> GetArticleById(Guid Id);

        Task<bool> DeleteArticle(Guid Id);



    }
}