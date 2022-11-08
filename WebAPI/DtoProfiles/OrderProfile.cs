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
            //OrderStatus and User are complex types so, map them to simple type using for member
            CreateMap<Order, OrderDto>()
                    .ForMember(dest => dest.Status, act => act.MapFrom(src => src.Status))
                    .ForMember(dest => dest.UserEmail, act => act.MapFrom(src => src.User.Email))
                    .ReverseMap();
        }
       
    }
}

