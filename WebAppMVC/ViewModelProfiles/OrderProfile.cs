using AutoMapper;
using WebApiClient.DTOs;
using WebAppMVC.ViewModels;

namespace WebAppMVC.ViewModelProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<LineItemDto, LineItemVM>()
                .ReverseMap();

            CreateMap<OrderDto, OrderDetailsVM>()
                .ReverseMap();

            CreateMap<OrderDto, OrderCreateVM>()
                .ReverseMap();

            CreateMap<IEnumerable<OrderDto>, OrderIndexVM>()
            .ForMember(dest => dest.Orders, act => act.MapFrom(src => src));

            

        }
    }
}
