using AutoMapper;
using TechchainDAL.Models;
using TechchainDAL.Models.DTO;

namespace TechchainBL.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Article, ArticleDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Username))
                .ForSourceMember(src => src.Rating, opt => opt.DoNotValidate());
        }
    }
}
