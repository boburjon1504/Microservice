using Microservice.Web.Models;
using Microservice.Web.Services;
using Microservice.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace Microservice.Web.Controllers
{
    public class HomeController(IProductService productService) : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public async  Task<IActionResult> Index()
        {
            List<ProductDto>? products = new List<ProductDto>();

            var response = await productService.GetAllProductsAsync();

            if (response is not null && response.IsSuccess)
            {
                products = JsonSerializer.Deserialize<List<ProductDto>>(Convert.ToString(response.Result), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            else
            {
                TempData["error"] = response.Message;
            }

            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await productService.GetByIdAsync(id);

            if (response is not null && response.IsSuccess)
            {
                var product = JsonSerializer.Deserialize<ProductDto>(Convert.ToString(response.Result), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return View(product);
            }
            else
            {
                TempData["error"] = response.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
