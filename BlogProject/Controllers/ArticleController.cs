using BlogBL;
using BlogDAL.Models;
using BlogDAL.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [Route("{id}")]
        [HttpGet]
        public async Task<Article> GetArticleById(Guid id)
        {
            Article result = await _service.GetArticleById(id);
            return result;
        }

        [HttpPost]
        public async Task<Boolean> CreateArticle([FromBody] ArticleDTO input)
        {
            var result = await _service.CreateArticle(input);
            return result;
        }
    }
}
