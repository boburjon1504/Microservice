using Microsoft.AspNetCore.Identity;

namespace Microservice.AuthApi.Models;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
}
