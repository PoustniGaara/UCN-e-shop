using AutoMapper;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using System.Diagnostics;
using WebApiClient.DTOs;
using WebAppMVC.ViewModels;

namespace WebAppMVC.ViewModelProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<IEnumerable<GetProductDto>, ProductIndexVM>()
            .ForMember(dest => dest.Products, act => act.MapFrom(src => src));
        }

 
    }
}
