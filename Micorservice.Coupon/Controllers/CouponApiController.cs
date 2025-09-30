using AutoMapper;
using Micorservice.CouponApi.Data;
using Micorservice.CouponApi.Models;
using Micorservice.CouponApi.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design.Serialization;

namespace Micorservice.CouponApi.Controllers;

[Route("api/coupon")]
[ApiController]
public class CouponApiController(AppDbContext dbContext, IMapper mapper)
    : ControllerBase
{
    private ResponseDto response = new ResponseDto();
    [HttpGet()]
    public async Task<ResponseDto> GetAll(CancellationToken cancellationToken)
    {
        IEnumerable<Coupon> coupons = await dbContext.Coupons.ToListAsync(cancellationToken);


        response.Result = mapper.Map<IEnumerable<CouponDto>>(coupons);
        response.IsSuccess = true;

        return response;
    }

    [HttpGet("{id:int}")]
    public async Task<ResponseDto> GetById(int id, CancellationToken cancellationToken)
    {
        Coupon? coupon = await dbContext.Coupons
                                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        response.IsSuccess = coupon is not null;

        response.Result = mapper.Map<CouponDto>(coupon);

        return response;
    }

    [HttpGet("getmycode/{code}")]
    public async Task<ResponseDto> GetByCode(string code, CancellationToken cancellationToken)
    {
        Coupon? coupon = await dbContext.Coupons
                                .FirstOrDefaultAsync(c => c.CouponCode == code, cancellationToken);

        response.IsSuccess = coupon is not null;
        response.Result = mapper.Map<CouponDto>(coupon);

        return response;
    }

    [HttpPost]
    public async Task<ResponseDto> Create([FromBody] CouponDto model, CancellationToken cancellationToken)
    {
        Coupon coupon = mapper.Map<Coupon>(model);

        await dbContext.Coupons.AddAsync(coupon);

        await dbContext.SaveChangesAsync(cancellationToken);

        response.IsSuccess = true;
        response.Result = mapper.Map<CouponDto>(coupon);

        return response;
    }

    [HttpPut]
    public async Task<ResponseDto> Update([FromBody] CouponDto model, CancellationToken cancellationToken)
    {
        Coupon coupon = mapper.Map<Coupon>(model);

        dbContext.Coupons.Update(coupon);

        await dbContext.SaveChangesAsync(cancellationToken);

        response.Result = mapper.Map<CouponDto>(coupon);
        response.IsSuccess = true;


        return response;
    }

    [HttpDelete("{id:int}")]
    public async Task<ResponseDto> Delete(int id, CancellationToken cancellationToken)
    {
        Coupon? coupon = await dbContext.Coupons.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (coupon is null)
            return new ResponseDto { IsSuccess = false };


        dbContext.Coupons.Remove(coupon);

        await dbContext.SaveChangesAsync(cancellationToken);

        response.Result = mapper.Map<CouponDto>(coupon);
        response.IsSuccess = true;

        return response;
    }

}
