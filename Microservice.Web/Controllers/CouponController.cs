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
        else
        {
            TempData["error"] = response?.Message;
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

            if(response is not null && response.IsSuccess)
            {
                TempData["success"] = "Coupon created succesfully";

                return RedirectToAction(nameof(CouponIndex));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
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
        else
        {
            TempData["error"] = response?.Message;
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
        else
        {
            TempData["error"] = response?.Message;
        }

        return View(couponDto);
    }
}
