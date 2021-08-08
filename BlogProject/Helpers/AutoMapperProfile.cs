using AutoMapper;
using BlogDAL.Models;
using BlogDAL.Models.DTO;

namespace BlogProject.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Article, ArticleDTO>();
        }
    }
}
