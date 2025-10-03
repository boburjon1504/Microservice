using Microservice.ShoppingCartApi.Models.Dto;
using Microservice.ShoppingCartApi.Services.Interfaces;
using System.Text.Json;

namespace Microservice.ShoppingCartApi.Services;

public class CouponService(IHttpClientFactory httpClientFactory) : ICouponService
{
    public async Task<CouponDto> GetCoupon(string couponCode)
    {
        var client = httpClientFactory.CreateClient("Coupon");

        var response = await client.GetAsync("api/coupon/getmycode/" + couponCode);

        var apiContent =  await response.Content.ReadAsStringAsync();

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var resp = JsonSerializer.Deserialize<ResponseDto>(apiContent, jsonOptions);

        if (resp.IsSuccess)
        {
            var couponDto = JsonSerializer.Deserialize<CouponDto>(Convert.ToString(resp.Result), jsonOptions);

            return couponDto;
        }

        return new();
    }
}
