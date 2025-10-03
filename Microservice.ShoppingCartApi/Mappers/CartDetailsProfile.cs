using AutoMapper;
using Microservice.ShoppingCartApi.Models;
using Microservice.ShoppingCartApi.Models.Dto;

namespace Microservice.ShoppingCartApi.Mappers;

public class CartDetailsProfile : Profile
{
    public CartDetailsProfile()
    {
        CreateMap<CartDetails, CartDetailsDto>()
                    .ReverseMap();
    }
}
