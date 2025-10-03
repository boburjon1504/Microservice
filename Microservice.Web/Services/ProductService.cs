using Microservice.Web.Models;
using Microservice.Web.Services.Interfaces;
using Microservice.Web.Utility;
using static Microservice.Web.Utility.SD;

namespace Microservice.Web.Services;

public class ProductService(IBaseService baseService) : IProductService
{
    public async Task<ResponseDto?> CreateAsync(ProductDto productDto)
    {
        return await baseService.SendAsync(new RequestDto
        {
            ApiType = ApiType.POST,
            Url = SD.ProductApiBase + "api/products/",
            Data = productDto
        });
    }

    public async Task<ResponseDto?> DeleteAsync(int id)
    {
        return await baseService.SendAsync(new RequestDto
        {
            ApiType = ApiType.DELETE,
            Url = SD.ProductApiBase + "api/products/" + id,
        });
    }

    public async Task<ResponseDto?> GetAllProductsAsync()
    {
        return await baseService.SendAsync(new RequestDto
        {
            ApiType = ApiType.GET,
            Url = SD.ProductApiBase + "api/products/",
        });
    }

    public async Task<ResponseDto?> GetByIdAsync(int id)
    {
        return await baseService.SendAsync(new RequestDto
        {
            ApiType = ApiType.GET,
            Url = SD.ProductApiBase + "api/products/" + id,
        });
    }

    public async Task<ResponseDto?> UpdateAsync(ProductDto productDto)
    {
        return await baseService.SendAsync(new RequestDto
        {
            ApiType = ApiType.PUT,
            Url = SD.ProductApiBase + "api/products/",
            Data = productDto
        });
    }
}
