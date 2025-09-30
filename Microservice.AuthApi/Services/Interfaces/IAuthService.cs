using Microservice.AuthApi.Models.Dto;

namespace Microservice.AuthApi.Services.Interfaces;

public interface IAuthService
{
    Task<string> RegisterAsync(RegistrationRequestDto registrationRequestDto);

    Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto);

    Task<bool> AssignRoleAsync(string email, string roleName);
}
