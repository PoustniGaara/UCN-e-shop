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
                .ReverseMap();

            CreateMap<Order, OrderDto>()
                    .ForMember(dest => dest.Status, act => act.MapFrom(src => src.Status))
                    .ForMember(dest => dest.UserEmail, act => act.MapFrom(src => src.User.Email))
                    .ReverseMap();

            //CreateMap<OrderDto, Order>()
            //    .ForMember(dest => dest.Status, act => act.MapFrom(src => src.Status))
            //    .ForPath(dest => dest.User.Email, act => act.MapFrom(src => src.UserEmail));

            //CreateMap<LineItemDto, LineItem>()
            //    .ForPath(dest => dest.Product.Name, act => act.MapFrom(src => src.ProductName))
            //    .ForPath(dest => dest.Product.Id, act => act.MapFrom(src => src.ProductId))
            //    .ForPath(dest => dest.Product.Price, act => act.MapFrom(src => src.Price));

            //CreateMap<OrderDto, Order>()
            //        .ForMember(dest => dest.Status, act => act.MapFrom(src => src.Status))
            //        .ForPath(dest => dest.User.Email, act => act.MapFrom(src => src.UserEmail));



        }
    }
}

