using System;
using AutoMapper;
using DataAccessLayer.Model;
using WebApi.DTOs;

namespace WebApi.DtoProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<LineItem, LineItemDto>()
                .ForMember(dest => dest.ProductName, act => act.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Price, act => act.MapFrom(src => src.Product.Price))
                .ReverseMap();

            CreateMap<Order, OrderDto>()
                    .ForMember(dest => dest.Status, act => act.MapFrom(src => src.Status))
                    .ForMember(dest => dest.UserEmail, act => act.MapFrom(src => src.User.Email))
                    .ReverseMap();


        }
    }
}

