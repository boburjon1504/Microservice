using AutoMapper;
using Microservice.ProductApi.Models;
using Microservice.ProductApi.Models.Dtos;

namespace Microservice.ProductApi.Mappers;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>()
                .ReverseMap();
    }
}
