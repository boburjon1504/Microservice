using Microservice.Web.Models;
using Microservice.Web.Services.Interfaces;
using Microservice.Web.Utility;
using static Microservice.Web.Utility.SD;

namespace Microservice.Web.Services;

public class AuthService(IBaseService baseService) : IAuthService
{
    public async Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto)
    {
        return await baseService.SendAsync(new RequestDto
        {
            ApiType = ApiType.POST,
            Url = SD.AuthApiBase + "api/auth/login",
            Data = registrationRequestDto
        });
    }

    public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
    {
        return await baseService.SendAsync(new RequestDto
        {
            ApiType = ApiType.POST,
            Url = SD.AuthApiBase + "api/auth/login",
            Data = loginRequestDto
        });
    }

    public async Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto)
    {
        return await baseService.SendAsync(new RequestDto
        {
            ApiType = ApiType.POST,
            Url = SD.AuthApiBase + "api/auth/register",
            Data = registrationRequestDto
        });
    }
}
