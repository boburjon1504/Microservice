using Microservice.Web.Models;

namespace Microservice.Web.Services.Interfaces;

public interface IProductService
{
    Task<ResponseDto?> GetAllProductsAsync();
    Task<ResponseDto?> GetByIdAsync(int id);
    Task<ResponseDto?> CreateAsync(ProductDto productDto);
    Task<ResponseDto?> UpdateAsync(ProductDto productDto);
    Task<ResponseDto?> DeleteAsync(int id);
}
