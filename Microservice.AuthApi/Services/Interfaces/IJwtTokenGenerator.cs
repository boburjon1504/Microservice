using Microservice.AuthApi.Models;
using System.Collections.Generic;

namespace Microservice.AuthApi.Services.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(ApplicationUser user, IEnumerable<string> roles);
}
