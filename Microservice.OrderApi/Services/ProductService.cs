using Microservice.OrderApi.Models.Dtos;
using Microservice.OrderApi.Services.Interfaces;
using System.Text.Json;

namespace Microservice.OrderApi.Services;

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
