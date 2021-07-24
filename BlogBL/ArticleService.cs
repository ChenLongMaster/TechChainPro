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
        public IConfiguration _configuration;
        private BlogContext _blogContext;
        public ArticleService(BlogContext blogContext, IConfiguration configuration)
        {
            _blogContext = blogContext;
            _configuration = configuration;
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
            var imageUrl = UploadImage(model.representImage);

            var entity = new Article()
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Category = model.Category,
                Abstract = model.Abstract,
                DisplayContent = model.DisplayContent,
                RepresentImageUrl = imageUrl,
                CreatedBy = model.CreatedBy,
                CreatedOn = DateTime.Now,
            };

            await _blogContext.Articles.AddAsync(entity);

            var result = await _blogContext.SaveChangesAsync();
            return result > 0;
        }

        public string UploadImage(IFormFile image)
        {
            string uniqueName = null;
            if (image is not null)
            {
                string uploadFolder = Path.Combine(_configuration["ImgFolder"]);
                uniqueName = $"{image.Name}_{Guid.NewGuid().ToString()}";
                string filePath = Path.Combine(uploadFolder, uniqueName);
                using (var fileStram = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStram);
                }
            }

            return uniqueName;
        }
    }
}
