using BlogBL;
using BlogDAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var result = await _service.GetArticleById(id);
            return result;
        }
    }
}
