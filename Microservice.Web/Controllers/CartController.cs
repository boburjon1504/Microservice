using Microservice.Web.Models;
using Microservice.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace Microservice.Web.Controllers
{
    public class CartController(ICartService cartService) : Controller
    {
        [Authorize]
        public async Task<IActionResult> CartIndex()
        {

            return View(await GetUserCart());
        }

        [Authorize]
        public async Task<IActionResult> Checkout()
        {

            return View(await GetUserCart());
        }

        public async Task<IActionResult> Remove(int cartDetailsId)
        {
            var response = await cartService.RemoveFromCart(cartDetailsId);

            if (response is not null && response.IsSuccess)
            {
                TempData["success"] = "Cart updated successfully";
            }
            else
            {
                TempData["error"] = response.Message;
            }

            return RedirectToAction(nameof(CartIndex));
        }

        public async Task<IActionResult> ApplyCoupon(CartDto cartDto)
        {
            var response = await cartService.ApplyCoupon(cartDto);

            if (response is not null && response.IsSuccess)
            {
                TempData["success"] = "Cart updated successfully";
            }
            else
            {
                TempData["error"] = response.Message;
            }

            return RedirectToAction(nameof(CartIndex));
        }

        private async Task<CartDto> GetUserCart()
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub).FirstOrDefault()?.Value;

            var response = await cartService.GetCart(userId);

            if (response is not null && response.IsSuccess)
            {
                var cart = JsonSerializer.Deserialize<CartDto>(Convert.ToString(response.Result), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return cart;
            }

            return new();
        }
    }
}
