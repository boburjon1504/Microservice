using Microservice.AuthApi.Models;

namespace Microservice.AuthApi.Services.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(ApplicationUser user);
}
