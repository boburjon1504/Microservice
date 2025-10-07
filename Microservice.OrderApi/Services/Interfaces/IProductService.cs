using Microservice.OrderApi.Models.Dtos;
namespace Microservice.OrderApi.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetProducts();
}
