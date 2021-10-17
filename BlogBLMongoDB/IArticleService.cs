using BlogDAL.Models.DTO;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogBL
{
    public interface IArticleService
    {
        Task<bool> CreateArticle(ArticleDTO model);
        Task<bool> DeleteArticle(int id);
        Task<ArticleDTO> GetArticleById(int id);
        Task<IEnumerable<ArticleDTO>> GetArticles(ArticleFilter filter);
        Task<IEnumerable<ArticleDTO>> GetRecommendedArticles();
        Task<bool> UpdateArticle(ArticleDTO model);
    }
}