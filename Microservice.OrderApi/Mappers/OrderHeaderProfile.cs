using AutoMapper;
using Microservice.OrderApi.Models;
using Microservice.OrderApi.Models.Dtos;

namespace Microservice.OrderApi.Mappers;

public class OrderHeaderProfile : Profile
{
    public OrderHeaderProfile()
    {
        CreateMap<OrderHeader, CartHeaderDto>()
            .ForMember(d => d.CartTotal, src => src.MapFrom(s => s.OrderTotal))
            .ReverseMap();

        CreateMap<OrderHeader, OrderHeaderDto>()
                .ReverseMap();
    }
}
