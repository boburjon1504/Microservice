using Microservice.AuthApi.Models.Dto;
using Microservice.AuthApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.AuthApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthApiController(IAuthService authService) : ControllerBase
    {
        private ResponseDto responseDto = new();
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {
            var errorMessage = await authService.RegisterAsync(model);

            responseDto.IsSuccess = errorMessage.Contains("success");
            responseDto.Message = errorMessage;

            return responseDto.IsSuccess ? Ok(responseDto) : BadRequest(responseDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var loginResponse = await authService.LoginAsync(model);

            if(loginResponse.User is null)
            {
                responseDto.IsSuccess = false;
                responseDto.Message = "Username or password is incorrect";

                return BadRequest(responseDto);
            }
            responseDto.IsSuccess = true;
            responseDto.Result = loginResponse;

            return Ok(responseDto);
        }
    }
}
