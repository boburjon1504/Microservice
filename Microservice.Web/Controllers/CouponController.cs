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
        
        if(response is not null && response.IsSuccess)
        {
            list = JsonSerializer.Deserialize<List<CouponDto>>(Convert.ToString(response.Result), new JsonSerializerOptions { PropertyNameCaseInsensitive = true});
        }
        return View(list);
    }
}
