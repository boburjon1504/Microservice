namespace Micorservice.CouponApi.Models.Dto;

public class ResponseDto<TData>
{
    public TData? Resule { get; set; }

    public bool IsSuccess { get; set; }

    public string? Message { get; set; }
}
