using Microservice.Web.Models;
using Microservice.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Microservice.Web.Controllers;

public class CouponController(ICouponService couponService) : Controller
{
    public async Task<IActionResult> CouponIndex()
    {
        List<CouponDto>? list = new();

        ResponseDto? response = await couponService.GetAllCouponsAsync();

        if (response is not null && response.IsSuccess)
        {
            list = JsonSerializer.Deserialize<List<CouponDto>>(Convert.ToString(response.Result), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        return View(list);
    }

    public async Task<IActionResult> CouponCreate()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CouponCreate(CouponDto model)
    {
        if (ModelState.IsValid)
        {
            ResponseDto? response = await couponService.CreateAsync(model);

            return RedirectToAction(nameof(CouponIndex));
        }

        return View(model);
    }

    public async Task<IActionResult> CouponDelete(int couponId)
    {
        ResponseDto? response = await couponService.GetByIdAsync(couponId);

        if(response is not null && response.IsSuccess)
        {
            CouponDto? model = JsonSerializer.Deserialize<CouponDto>(Convert.ToString(response.Result), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(model);
        }

        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CouponDelete(CouponDto couponDto)
    {
        ResponseDto? response = await couponService.DeleteAsync(couponDto.Id);

        if (response is not null && response.IsSuccess)
        {
            return RedirectToAction(nameof(CouponIndex));
        }

        return View(couponDto);
    }
}
