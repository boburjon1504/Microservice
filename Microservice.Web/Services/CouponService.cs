using Microservice.Web.Models;
using Microservice.Web.Services.Interfaces;
using static Microservice.Web.Utility.SD;

namespace Microservice.Web.Services;

public class CouponService(IBaseService baseService) : ICouponService
{
    public async Task<ResponseDto?> CreateAsync(CouponDto couponDto)
    {
        return await baseService.SendAsync(new RequestDto
        {
            ApiType = ApiType.POST,
            Url = "/api/coupon",
            Data = couponDto
        });
    }

    public async Task<ResponseDto?> DeleteAsync(int id)
    {
        return await baseService.SendAsync(new RequestDto
        {
            ApiType = ApiType.DELETE,
            Url = "/api/coupon/" + id
        });
    }

    public async Task<ResponseDto?> GetAllCouponsAsync()
    {
        return await baseService.SendAsync(new RequestDto
        {
            ApiType = ApiType.GET,
            Url = "api/couponapi"
        });
    }

    public async Task<ResponseDto?> GetByIdAsync(int id)
    {
        return await baseService.SendAsync(new RequestDto
        {
            ApiType = ApiType.GET,
            Url = "/api/coupon/" + id
        });
    }

    public async Task<ResponseDto?> GetCouponAsync(string couponCode)
    {
        return await baseService.SendAsync(new RequestDto
        {
            ApiType = ApiType.GET,
            Url = "/api/coupon/getmycode" + couponCode
        });
    }

    public async Task<ResponseDto?> UpdateAsync(CouponDto couponDto)
    {
        return await baseService.SendAsync(new RequestDto
        {
            ApiType = ApiType.PUT,
            Url = "/api/coupon",
            Data = couponDto
        });
    }
}
