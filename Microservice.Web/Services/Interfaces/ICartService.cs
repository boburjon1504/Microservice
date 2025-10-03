using Microservice.Web.Models;

namespace Microservice.Web.Services.Interfaces;

public interface ICartService
{
    Task<ResponseDto?> GetCart(string userId);

    Task<ResponseDto?> Upsert(CartDto cart);

    Task<ResponseDto?> RemoveFromCart(string cartDetailsId);

    Task<ResponseDto?> ApplyCoupon(CartDto cart);
}
