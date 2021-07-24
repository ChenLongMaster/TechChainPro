using BlogDAL.Models;
using BlogDAL.Models.DTO;
using System;
using System.Threading.Tasks;

namespace BlogBL
{
    public interface IArticleService
    {
        Task<Article> GetArticleById(Guid Id);
        Task<Boolean> CreateArticle(ArticleDTO model);

    }
}