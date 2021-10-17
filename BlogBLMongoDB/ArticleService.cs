using AutoMapper;
using BlogBL.Helpers;
using BlogDAL.Models;
using BlogDAL.Models.DTO;
using BlogDAL.Uow;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogBL
{
    public class ArticleService : IArticleService
    {
        private readonly IDBClient _dbClient;
        private readonly IMapper _mapper;
        //private readonly HttpContext _context;
        private ClaimsPrincipal _principal;
        public ArticleService(IDBClient dbClient, IMapper mapper, ClaimsPrincipal principal)
        {
            _dbClient = dbClient;
            _mapper = mapper;
            _principal = principal;
        }
        public async Task<IEnumerable<ArticleDTO>> GetArticles(ArticleFilter filter)
        {
            var builder = Builders<Article>.Filter;
            var query = builder.Where(x => x.IsDeleted == false);

            if (filter.CategoryId != 1)
            {
                query = query & builder.Where(x => x.CategoryId == filter.CategoryId);
            }

            var filterResult = _dbClient.GetArticleContext().Find(query);

            if (filter.SortDateDirection == (int)SortDirectionCustom.ASC)
            {
                filterResult = filterResult.SortBy(x => x.CreatedOn);
            }
            else
            {
                filterResult = filterResult.SortByDescending(x => x.CreatedOn);
            }
            var entity = await filterResult.ToListAsync();
            var deto = _mapper.Map<IEnumerable<ArticleDTO>>(entity);

            return deto;
        }
        public async Task<ArticleDTO> GetArticleById(int id)
        {
            var entity = await _dbClient.GetArticleContext().FindSync(x => x.Id == id && x.IsDeleted == false).SingleOrDefaultAsync();
            var result = _mapper.Map<ArticleDTO>(entity);

            return result;
        }

        public async Task<IEnumerable<ArticleDTO>> GetRecommendedArticles()
        {
            var entity = await _dbClient.GetArticleContext().Find(x => x.IsDeleted == false)
                                                    .Limit(5)
                                                    .SortByDescending(x => x.Rating).ToListAsync();
            var result = _mapper.Map<IEnumerable<ArticleDTO>>(entity);
            return result;
        }

        public async Task<bool> CreateArticle(ArticleDTO model)
        {
            var authorId = _principal.FindFirst("id").Value;
            var author = await _dbClient.GetUserContext().FindSync(x => x.Id == authorId).FirstOrDefaultAsync();
            var category = await _dbClient.GetCategoryContext().FindSync(x => x.Id == model.CategoryId).FirstOrDefaultAsync();

            var entity = new Article()
            {
                Id = getNextArticleId(),
                Name = model.Name,
                Abstract = model.Abstract,
                DisplayContent = model.DisplayContent,
                RepresentImageUrl = model.RepresentImageUrl,
                Author = author,
                AuthorId = authorId.ToString(),
                Category = category,
                CategoryId = model.CategoryId,
                CreatedOn = DateTime.Now,
            };

            await _dbClient.GetArticleContext().InsertOneAsync(entity);
            return true;
        }

        public async Task<bool> UpdateArticle(ArticleDTO model)
        {
            var authorId = _principal.FindFirst("id").Value;

            var entity = await _dbClient.GetArticleContext().FindSync(x => x.Id == model.Id).SingleOrDefaultAsync();

            if (authorId != entity.AuthorId.ToString())
            {
                throw new Exception("You are not the author!.");
            }

            if (entity is not null)
            {
                entity.Abstract = model.Abstract;
                entity.Name = model.Name;
                entity.CategoryId = model.CategoryId;
                entity.DisplayContent = model.DisplayContent;
                entity.RepresentImageUrl = model.RepresentImageUrl;
            }

            await _dbClient.GetArticleContext().ReplaceOneAsync(x => x.Id == model.Id, entity);

            return true;
        }
        public async Task<bool> DeleteArticle(int id)
        {
            var entity = await _dbClient.GetArticleContext().FindSync(x => x.Id == id).SingleOrDefaultAsync();
            var authorId = _principal.FindFirst("id").Value;

            if (authorId != entity.AuthorId.ToString())
            {
                throw new Exception("You are not the author!.");
            }

            entity.IsDeleted = true;
            await _dbClient.GetArticleContext().ReplaceOneAsync(x => x.Id == id, entity);

            return true;
        }

        public int? getNextArticleId()
        {
            var newestArticle = _dbClient.GetArticleContext().Find(x => true).SortByDescending(x => x.Id).Limit(1).SingleOrDefault();
            if(newestArticle is null)
            {
                return 0;
            }
            var maxId = newestArticle.Id;
            return maxId += 1;
        }
    }
}
