using Microservice.Web.Models;
using Microservice.Web.Services.Interfaces;
using Microservice.Web.Utility;
using static Microservice.Web.Utility.SD;

namespace Microservice.Web.Services;

public class CartService(IBaseService baseService) : ICartService
{
    public async Task<ResponseDto?> ApplyCoupon(CartDto cart)
    {
        return await baseService.SendAsync(new RequestDto
        {
            Data = cart,
            Url = SD.ShoppingCartApiBase + "api/cart/applycoupon",
            ApiType = ApiType.POST,
        });
    }

    public async Task<ResponseDto?> GetCart(string userId)
    {
        return await baseService.SendAsync(new RequestDto
        {
            Url = SD.ShoppingCartApiBase + "api/cart/" + userId,
            ApiType = ApiType.GET,
        });
    }

    public async Task<ResponseDto?> RemoveFromCart(int cartDetailsId)
    {
        return await baseService.SendAsync(new RequestDto
        {
            Data = cartDetailsId,
            Url = SD.ShoppingCartApiBase + "api/cart/removecart",
            ApiType = ApiType.POST,
        });
    }

    public async Task<ResponseDto?> Upsert(CartDto cart)
    {
        return await baseService.SendAsync(new RequestDto
        {
            Data = cart,
            Url = SD.ShoppingCartApiBase + "api/cart/upsert",
            ApiType = ApiType.POST,
        }); ;
    }
}
