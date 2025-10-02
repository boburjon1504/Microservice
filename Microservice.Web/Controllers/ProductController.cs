using Microservice.Web.Models;
using Microservice.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;

namespace Microservice.Web.Controllers
{
    public class ProductController(IProductService productService) : Controller
    {
        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDto>? products = new List<ProductDto>();

            var response = await productService.GetAllProductsAsync();

            if(response is not null && response.IsSuccess)
            {
                products = JsonSerializer.Deserialize<List<ProductDto>>(Convert.ToString(response.Result), new JsonSerializerOptions { PropertyNameCaseInsensitive = true});
            }
            else
            {
                TempData["error"] = response.Message;
            }

                return View(products);
        }
    }
}
