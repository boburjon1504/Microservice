using Microservice.Web.Models;

namespace Microservice.Web.Services.Interfaces;

public interface ICouponService
{
    Task<ResponseDto?> GetCouponAsync(string couponCode);
    Task<ResponseDto?> GetAllCouponsAsync();
    Task<ResponseDto?> GetByIdAsync(int id);
    Task<ResponseDto?> CreateAsync(CouponDto couponDto);
    Task<ResponseDto?> UpdateAsync(CouponDto couponDto);
    Task<ResponseDto?> DeleteAsync(int id);
}
