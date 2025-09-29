using Micorservice.CouponApi.Data;
using Micorservice.CouponApi.Models;
using Micorservice.CouponApi.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Micorservice.CouponApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CouponApiController(AppDbContext dbContext) : ControllerBase
{
    [HttpGet()]
    public async Task<ResponseDto<IEnumerable<Coupon>>> GetAll(CancellationToken cancellationToken)
    {
        IEnumerable<Coupon> coupons = await dbContext.Coupons.ToListAsync(cancellationToken);

        var response = new ResponseDto<IEnumerable<Coupon>>();

        response.Result = coupons;

        return response;
    }

    [HttpGet("{id:int}")]
    public async Task<ResponseDto<Coupon>> GetById(int id, CancellationToken cancellationToken)
    {
        Coupon? coupon = await dbContext.Coupons
                                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        var response = new ResponseDto<Coupon>();

        response.Result = coupon;

        return response;
    }
}
