using Microservice.Web.Models;
using Microservice.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Web.Controllers
{
    public class AuthController(IAuthService authService) : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new LoginRequestDto();

            return View(loginRequestDto);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            return View();
        }
    }
}
