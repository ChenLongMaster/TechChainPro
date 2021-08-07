using BlogBL;
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
        [Authorize(Policy = PolicyConstants.CreateArticle)]
        public async Task<bool> CreateArticle([FromBody] ArticleDTO input)
        {
            bool result = await _service.CreateArticle(input);
            return true;
        }

        [Route("{id}")]
        [HttpGet]
        [Authorize(Policy = PolicyConstants.ViewArticle)]
        public async Task<Article> GetArticleById(Guid id)
        {
            Article result = await _service.GetArticleById(id);
            return result;
        }

        [HttpGet]
        public async Task<IEnumerable<ArticleDTO>> GetArticle([FromQuery] ArticleFilter filter)
        {
            IEnumerable<ArticleDTO> result = await _service.GetArticles(filter);
            return result;
        }

        [Route("recommended")]
        [HttpGet]
        public async Task<IEnumerable<ArticleDTO>> GetRecommendedArticle([FromQuery] ArticleFilter filter)
        {
            IEnumerable<ArticleDTO> result = await _service.GetRecommendedArticles();
            return result;
        }

        [HttpPut]
        [Authorize(Policy = PolicyConstants.EditArticle)]
        public async Task<bool> UpdateArticleById([FromBody] ArticleDTO model)
        {
            bool result = await _service.UpdateArticle(model);
            return result;
        }
    }
}
