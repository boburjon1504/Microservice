using Microservice.Web.Models;

namespace Microservice.Web.Services.Interfaces;

public interface IOrderService
{
    Task<ResponseDto?> CreateOrderAsync(CartDto cart);
}
