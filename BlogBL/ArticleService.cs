using AutoMapper;
using BlogDALOld.Models;
using BlogDALOld.Models.DTO;
using BlogDALOld.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogBLOld
{
    public class ArticleService : IArticleService
    {
        private BlogContext _blogContext;
        private readonly IMapper _mapper;
        //private readonly HttpContext _context;
        private ClaimsPrincipal _principal;
        public ArticleService(BlogContext blogContext, IMapper mapper, ClaimsPrincipal principal)
        {
            _blogContext = blogContext;
            _mapper = mapper;
            _principal = principal;
        }
        public async Task<IEnumerable<ArticleDTO>> GetArticles(ArticleFilter filter)
        {
            var query = _blogContext.Articles.Include(x => x.Author).Include(x => x.Category).Where(x => x.IsDeleted == false).AsSingleQuery();

            if (filter.CategoryId != 1)
            {
                query = query.Where(x => x.CategoryId == filter.CategoryId);
            }

         
            else
            {
                query = query.OrderByDescending(x => x.CreatedOn);
            }

            var entity = await query.ToListAsync();
            var deto = _mapper.Map<IEnumerable<ArticleDTO>>(entity);
            #region use store procedure solution
            //var param = new SqlParameter("@CategoryId", filter.CategoryId);
            //entity = await _blogContext.ArticleDTO.FromSqlRaw("Exec GetAllArticles @CategoryId", param).ToListAsync();
            #endregion

            return deto;
        }
        public async Task<ArticleDTO> GetArticleById(int id)
        {
            var entity = await _blogContext.Articles.Include(x => x.Author).SingleAsync(x => x.Id == id && x.IsDeleted == false);
            var result = _mapper.Map<ArticleDTO>(entity);

            return result;
        }

        public async Task<IEnumerable<ArticleDTO>> GetRecommendedArticles()
        {
            var entity = await _blogContext.Articles.Where(x => x.IsDeleted == false)
                                                    .Take(5)
                                                    .OrderByDescending(x => x.Rating).ToListAsync();
            var result = _mapper.Map<IEnumerable<ArticleDTO>>(entity);
            return result;
        }

        public async Task<bool> CreateArticle(ArticleDTO model)
        {
            var authorId = _principal.FindFirst("id").Value;
            var entity = new Article()
            {
                Name = model.Name,
                CategoryId = model.CategoryId,
                Abstract = model.Abstract,
                DisplayContent = model.DisplayContent,
                RepresentImageUrl = model.RepresentImageUrl,
                AuthorId = new Guid(authorId),
                CreatedOn = DateTime.Now,
            };

            await _blogContext.Articles.AddAsync(entity);

            var result = await _blogContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UpdateArticle(ArticleDTO model)
        {
            var authorId = _principal.FindFirst("id").Value;

            var entity = await _blogContext.Articles.Include(x => x.Author).SingleOrDefaultAsync(x => x.Id == model.Id);

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

            _blogContext.Update(entity);
            var result = await _blogContext.SaveChangesAsync();

            return result > 0;
        }
        public async Task<bool> DeleteArticle(int id)
        {
            var entity = await _blogContext.Articles.Include(x => x.Author).SingleAsync(x => x.Id == id);
            var authorId = _principal.FindFirst("id").Value;

            if (authorId != entity.AuthorId.ToString())
            {
                throw new Exception("You are not the author!.");
            }

            entity.IsDeleted = true;
            _blogContext.Update(entity);
            var result = await _blogContext.SaveChangesAsync();

            return result > 0;
        }
    }
}
