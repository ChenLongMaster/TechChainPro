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

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<ArticleDTO> GetArticleById(Guid id)
        {
            var result = await _service.GetArticleById(id);
            return result;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<ArticleDTO>> GetArticle([FromQuery] ArticleFilter filter)
        {
            var result = await _service.GetArticles(filter);
            return result;
        }

        [Route("recommended")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<ArticleDTO>> GetRecommendedArticle([FromQuery] ArticleFilter filter)
        {
            var result = await _service.GetRecommendedArticles();
            return result;
        }

        [HttpPut]
        [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.Moderator + "," + RoleConstants.Member)]
        public async Task<bool> UpdateArticleById([FromBody] ArticleDTO model)
        {
            bool result = await _service.UpdateArticle(model);
            return result;
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.Member)]
        public async Task<bool> DeleteArticleById(Guid id)
        {
            bool result = await _service.DeleteArticle(id);
            return result;
        }
    }
}
