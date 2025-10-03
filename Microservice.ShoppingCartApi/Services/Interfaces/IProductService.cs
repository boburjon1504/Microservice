using Microservice.ShoppingCartApi.Models.Dto;
namespace Microservice.ShoppingCartApi.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetProducts();
}
