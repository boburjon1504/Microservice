using Microservice.Web.Models;
using Microservice.Web.Services.Interfaces;
using Microservice.Web.Utility;
using static Microservice.Web.Utility.SD;

namespace Microservice.Web.Services;

public class OrderService(IBaseService baseService) : IOrderService
{
    public Task<ResponseDto?> CreateOrderAsync(CartDto cart)
    {
        return baseService.SendAsync(new RequestDto
        {
            ApiType = ApiType.POST,
            Data = cart,
            Url = SD.OrderApiBase + "api/orders"
        });
    }
}
