using Microservice.Web.Models;
using Microservice.Web.Services.Interfaces;
using Microservice.Web.Utility;
using static Microservice.Web.Utility.SD;

namespace Microservice.Web.Services;

public class CouponService(IBaseService baseService) : ICouponService
{
    public async Task<ResponseDto?> CreateAsync(CouponDto couponDto)
    {
        return await baseService.SendAsync(new RequestDto
        {
            ApiType = ApiType.POST,
            Url = SD.CouponApiBase + "api/coupon",
            Data = couponDto
        });
    }

    public async Task<ResponseDto?> DeleteAsync(int id)
    {
        return await baseService.SendAsync(new RequestDto
        {
            ApiType = ApiType.DELETE,
            Url = SD.CouponApiBase + "api/coupon/" + id
        });
    }

    public async Task<ResponseDto?> GetAllCouponsAsync()
    {
        return await baseService.SendAsync(new RequestDto
        {
            ApiType = ApiType.GET,
            Url = SD.CouponApiBase + "api/coupon"
        });
    }

    public async Task<ResponseDto?> GetByIdAsync(int id)
    {
        return await baseService.SendAsync(new RequestDto
        {
            ApiType = ApiType.GET,
            Url = SD.CouponApiBase + "api/coupon/" + id
        });
    }

    public async Task<ResponseDto?> GetCouponAsync(string couponCode)
    {
        return await baseService.SendAsync(new RequestDto
        {
            ApiType = ApiType.GET,
            Url = SD.CouponApiBase + "api/coupon/getmycode" + couponCode
        });
    }

    public async Task<ResponseDto?> UpdateAsync(CouponDto couponDto)
    {
        return await baseService.SendAsync(new RequestDto
        {
            ApiType = ApiType.PUT,
            Url = SD.CouponApiBase + "api/coupon",
            Data = couponDto
        });
    }
}
