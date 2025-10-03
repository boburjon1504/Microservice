using Microservice.ShoppingCartApi.Models.Dto;
using Microservice.ShoppingCartApi.Services.Interfaces;
using System.Text.Json;

namespace Microservice.ShoppingCartApi.Services;

public class ProductService(IHttpClientFactory httpClientFactory) : IProductService
{
    public async Task<IEnumerable<ProductDto>> GetProducts()
    {
        var client = httpClientFactory.CreateClient("Product");

        var response = await client.GetAsync("api/products");

        var apiContent = await response.Content.ReadAsStringAsync();

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        var resp = JsonSerializer.Deserialize<ResponseDto>(apiContent, jsonOptions);

        if (resp.IsSuccess)
        {
            var products = JsonSerializer.Deserialize<IEnumerable<ProductDto>>(Convert.ToString(resp.Result), jsonOptions);

            return products;
        }

        return [];
    }
}
