using AutoMapper;
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
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BlogBL
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
            var entity = new List<ArticleDTO>();
            //var query = from a in _blogContext.Articles
            //            join c in _blogContext.Categories on a.CategoryId equals c.Id
            //            join u in _blogContext.Users on a.AuthorId equals u.Id into x
            //            from subUser in x.DefaultIfEmpty()
            //            where a.IsDeleted == false
            //            select new ArticleDTO
            //            {
            //                Id = a.Id,
            //                Name = a.Name,
            //                Abstract = a.Abstract,
            //                DisplayContent = a.DisplayContent,
            //                CategoryId = a.CategoryId,
            //                CategoryName = c.Name,
            //                RepresentImageUrl = a.RepresentImageUrl,
            //                AuthorName = subUser.Username,
            //                CreatedOn = a.CreatedOn
            //            };

            //if (filter.CategoryId != 1)
            //{
            //    query = from a in _blogContext.Articles
            //            join c in _blogContext.Categories on a.CategoryId equals c.Id
            //            join u in _blogContext.Users on a.AuthorId equals u.Id into x
            //            from subUser in x.DefaultIfEmpty()
            //            where a.CategoryId == filter.CategoryId && a.IsDeleted == false
            //            select new ArticleDTO
            //            {
            //                Id = a.Id,
            //                Name = a.Name,
            //                Abstract = a.Abstract,
            //                DisplayContent = a.DisplayContent,
            //                CategoryId = a.CategoryId,
            //                CategoryName = c.Name,
            //                RepresentImageUrl = a.RepresentImageUrl,
            //                AuthorName = subUser.Username,
            //                CreatedOn = a.CreatedOn
            //            };
            //}

            //if (filter.SortDateDirection == (int)SortDirection.ASC)
            //{
            //    entity = await query.OrderBy(x => x.CreatedOn).ToListAsync();
            //}
            //else
            //{
            //    entity = await query.OrderByDescending(x => x.CreatedOn).ToListAsync();
            //}

            #region use store procedure
            var param = new SqlParameter("@CategoryId", filter.CategoryId);
            var article = _blogContext.Articles.FromSqlRaw("Exec GetAllArticles @CategoryId", param).ToList();
            entity = _mapper.Map<List<ArticleDTO>>(article);
            #endregion

            return entity;
        }
        public async Task<ArticleDTO> GetArticleById(int id)
        {
            var entity = await _blogContext.Articles.Include(x => x.Author).SingleAsync(x => x.Id == id && x.IsDeleted == false);
            var result = _mapper.Map<ArticleDTO>(entity);

            return result;
        }

        public async Task<IEnumerable<ArticleDTO>> GetRecommendedArticles()
        {
            var entity = await _blogContext.Articles.Where(x => x.IsDeleted ==false)
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
