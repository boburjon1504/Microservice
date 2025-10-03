using Microservice.Web.Models;
using Microservice.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;

namespace Microservice.Web.Controllers;

public class ProductController(IProductService productService) : Controller
{
    public async Task<IActionResult> ProductIndex()
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

    public IActionResult ProductCreate()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ProductCreate(ProductDto model)
    {
        var response = await productService.CreateAsync(model);

        if (response is not null && response.IsSuccess)
        {
            TempData["success"] = "Product created successfully";

            return RedirectToAction(nameof(ProductIndex));
        }
        else
        {
            TempData["error"] = response.Message;
            return View(model);
        }
    }

    public async Task<IActionResult> ProductUpdate(int id)
    {
        ResponseDto? response = await productService.GetByIdAsync(id);

        if (response is not null && response.IsSuccess)
        {
            ProductDto? productDto = JsonSerializer.Deserialize<ProductDto>(Convert.ToString(response.Result), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(productDto);
        }
        else
        {
            TempData["error"] = response.Message;
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> ProductUpdate(ProductDto model)
    {
        var response = await productService.UpdateAsync(model);

        if (response is not null && response.IsSuccess)
        {
            TempData["success"] = "Product updated successfully";

            return RedirectToAction(nameof(ProductIndex));
        }
        else
        {
            TempData["error"] = response.Message;
            return View(model);
        }
    }

    public async Task<IActionResult> ProductDelete(int id)
    {
        ResponseDto? response = await productService.GetByIdAsync(id);

        if (response is not null && response.IsSuccess)
        {
            ProductDto? productDto = JsonSerializer.Deserialize<ProductDto>(Convert.ToString(response.Result), new JsonSerializerOptions { PropertyNameCaseInsensitive = true});

            return View(productDto);
        }
        else
        {
            TempData["error"] = response.Message;
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> ProductDelete(ProductDto model)
    {
        var response = await productService.DeleteAsync(model.Id);

        if (response is not null && response.IsSuccess)
        {
            TempData["success"] = "Product deleted successfully";

            return RedirectToAction(nameof(ProductIndex));
        }
        else
        {
            TempData["error"] = response.Message;
            return View(model);
        }
    }
}
