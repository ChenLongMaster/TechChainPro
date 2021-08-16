using AutoMapper;
using BlogDAL.Models;
using BlogDAL.Models.DTO;

namespace BlogBL.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Article, ArticleDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.Ignore())
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src=>src.Author.Username))
                .ForSourceMember(src => src.Rating, opt => opt.DoNotValidate());
        }
    }
}
