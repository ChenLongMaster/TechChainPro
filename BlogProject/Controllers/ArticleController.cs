using BlogBL;
using BlogDAL.Authorization;
using BlogDAL.Models;
using BlogDAL.Models.DTO;
using BlogDAL.Policies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : Controller
    {
        private readonly IArticleService _service;
        public ArticleController(IArticleService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.Member)]
        public async Task<bool> CreateArticle([FromBody] ArticleDTO input)
        {
            bool result = await _service.CreateArticle(input);
            return true;
        }

        [Route("{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<Article> GetArticleById(Guid id)
        {
            Article result = await _service.GetArticleById(id);
            return result;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<ArticleDTO>> GetArticle([FromQuery] ArticleFilter filter)
        {
            IEnumerable<ArticleDTO> result = await _service.GetArticles(filter);
            return result;
        }

        [Route("recommended")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<ArticleDTO>> GetRecommendedArticle([FromQuery] ArticleFilter filter)
        {
            IEnumerable<ArticleDTO> result = await _service.GetRecommendedArticles();
            return result;
        }

        [HttpPut]
        [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.Moderator)]
        public async Task<bool> UpdateArticleById([FromBody] ArticleDTO model)
        {
            bool result = await _service.UpdateArticle(model);
            return result;
        }

        [HttpDelete]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<bool> DeleteArticleById(Guid id)
        {
            bool result = await _service.DeleteArticle(id);
            return result;
        }
    }
}
