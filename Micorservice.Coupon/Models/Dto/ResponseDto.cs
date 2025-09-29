namespace Micorservice.CouponApi.Models.Dto;

public class ResponseDto<TData>
{
    public TData? Result { get; set; }

    public bool IsSuccess => Result is not null;

    public string? Message { get; set; }
}
