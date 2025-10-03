using Microservice.ShoppingCartApi.Models.Dto;

namespace Microservice.ShoppingCartApi.Services.Interfaces;

public interface ICouponService
{
    Task<CouponDto> GetCoupon(string couponCode);
}
