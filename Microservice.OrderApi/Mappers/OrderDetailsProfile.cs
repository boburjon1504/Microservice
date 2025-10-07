using AutoMapper;
using Microservice.OrderApi.Models;
using Microservice.OrderApi.Models.Dtos;

namespace Microservice.OrderApi.Mappers;

public class OrderDetailsProfile : Profile
{
    public OrderDetailsProfile()
    {
        CreateMap<OrderDetails, OrderDetailsDto>()
                .ReverseMap();

        CreateMap<CartDetailsDto, OrderDetails>()
            .ForMember(d => d.ProductName, src => src.MapFrom(s => s.Product.Name))
            .ForMember(d => d.ProductPrice, src => src.MapFrom(s => s.Product.Price));
    }
}
