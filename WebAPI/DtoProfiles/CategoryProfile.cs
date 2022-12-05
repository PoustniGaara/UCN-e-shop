using AutoMapper;
using DataAccessLayer.Model;
using WebApi.DTOs;

namespace WebApi.DtoProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>()
                .ReverseMap();
        }
    }
}
