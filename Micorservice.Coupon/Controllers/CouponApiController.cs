using AutoMapper;
using Micorservice.CouponApi.Data;
using Micorservice.CouponApi.Models;
using Micorservice.CouponApi.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Micorservice.CouponApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CouponApiController(AppDbContext dbContext, IMapper mapper)
    : ControllerBase
{
    [HttpGet()]
    public async Task<ResponseDto<IEnumerable<CouponDto>>> GetAll(CancellationToken cancellationToken)
    {
        IEnumerable<Coupon> coupons = await dbContext.Coupons.ToListAsync(cancellationToken);

        var response = new ResponseDto<IEnumerable<CouponDto>>();

        response.Result = mapper.Map<IEnumerable<CouponDto>>(coupons);

        return response;
    }

    [HttpGet("{id:int}")]
    public async Task<ResponseDto<CouponDto>> GetById(int id, CancellationToken cancellationToken)
    {
        Coupon? coupon = await dbContext.Coupons
                                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        var response = new ResponseDto<CouponDto>();

        response.Result = mapper.Map<CouponDto>(coupon);

        return response;
    }

    [HttpGet("getmycode/{code}")]
    public async Task<ResponseDto<CouponDto>> GetByCode(string code, CancellationToken cancellationToken)
    {
        Coupon? coupon = await dbContext.Coupons
                                .FirstOrDefaultAsync(c => c.CouponCode == code, cancellationToken);

        var response = new ResponseDto<CouponDto>();

        response.Result = mapper.Map<CouponDto>(coupon);

        return response;
    }

    [HttpPost]
    public async Task<ResponseDto<CouponDto>> Create([FromBody] CouponDto model, CancellationToken cancellationToken)
    {
        Coupon coupon = mapper.Map<Coupon>(model);

        await dbContext.Coupons.AddAsync(coupon);

        await dbContext.SaveChangesAsync(cancellationToken);

        var response = new ResponseDto<CouponDto>();

        response.Result = mapper.Map<CouponDto>(coupon);

        return response;
    }

    [HttpPut]
    public async Task<ResponseDto<CouponDto>> Update([FromBody] CouponDto model, CancellationToken cancellationToken)
    {
        Coupon coupon = mapper.Map<Coupon>(model);

        dbContext.Coupons.Update(coupon);

        await dbContext.SaveChangesAsync(cancellationToken);

        var response = new ResponseDto<CouponDto>();

        response.Result = mapper.Map<CouponDto>(coupon);

        return response;
    }

    [HttpDelete]
    public async Task<ResponseDto<CouponDto>> Delete(int id, CancellationToken cancellationToken)
    {
        Coupon? coupon = await dbContext.Coupons.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (coupon is null)
            return new ResponseDto<CouponDto>();


        dbContext.Coupons.Remove(coupon);

        await dbContext.SaveChangesAsync(cancellationToken);

        var response = new ResponseDto<CouponDto>();
        response.Result = mapper.Map<CouponDto>(coupon);

        return response;
    }

}
