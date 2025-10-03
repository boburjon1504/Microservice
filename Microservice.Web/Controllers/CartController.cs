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

        private async Task<CartDto> GetUserCart()
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub).FirstOrDefault()?.Value;

            var response = await cartService.GetCart(userId);
            
            if(response is not null && response.IsSuccess)
            {
                var cart = JsonSerializer.Deserialize<CartDto>(Convert.ToString(response.Result), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return cart;
            }

            return new();
        }
    }
}
