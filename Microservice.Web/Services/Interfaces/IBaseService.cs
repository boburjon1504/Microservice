using Microservice.Web.Models;

namespace Microservice.Web.Services.Interfaces;

public interface IBaseService
{
    Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true);
}
