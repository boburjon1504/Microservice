using Micorservice.CouponApi.Data;
using Micorservice.CouponApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Micorservice.CouponApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CouponApiController(AppDbContext dbContext) : ControllerBase
{
    [HttpGet()]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        IEnumerable<Coupon> coupons = await dbContext.Coupons.ToListAsync(cancellationToken);

        return Ok(coupons);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        Coupon? coupon = await dbContext.Coupons
                                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        return Ok(coupon);
    }
}
