namespace Microservice.Web.Models;

public class ResponseDto<TData>
{
    public TData? Result { get; set; }

    public bool IsSuccess => Result is not null;

    public string? Message { get; set; }
}
