using AutoMapper;
using Micorservice.CouponApi.Models;
using Micorservice.CouponApi.Models.Dto;

namespace Micorservice.CouponApi.Mappers;

public class CouponProfile : Profile
{
    public CouponProfile()
    {
        CreateMap<Coupon, CouponDto>()
                .ReverseMap();
    }
}
