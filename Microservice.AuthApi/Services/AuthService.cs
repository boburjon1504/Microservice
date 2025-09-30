using Microservice.AuthApi.Data;
using Microservice.AuthApi.Models;
using Microservice.AuthApi.Models.Dto;
using Microservice.AuthApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Microservice.AuthApi.Services;

public class AuthService(
    AppDbContext dbContext,
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager,
    IJwtTokenGenerator jwtTokenGenerator) : IAuthService
{
    public async Task<bool> AssignRoleAsync(string email, string roleName)
    {
        var user = dbContext.ApplicationUsers.FirstOrDefault(u => u.Email == email);

        if (user is not null)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole { Name =  roleName });
            }

            await userManager.AddToRoleAsync(user, roleName);

            return true;
        }

        return false;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
    {
        var user = dbContext.ApplicationUsers.FirstOrDefault(u => u.UserName == loginRequestDto.Username);

        if (user is null || !await userManager.CheckPasswordAsync(user, loginRequestDto.Password))
        {
            return new LoginResponseDto { User = null, Token = "" };
        }

        UserDto userDto = new()
        {
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Id = user.Id,
            Name = user.Name,
        };

        LoginResponseDto loginResponse = new LoginResponseDto
        {
            User = userDto,
            Token = jwtTokenGenerator.GenerateToken(user)
        };

        return loginResponse;
    }

    public async Task<string> RegisterAsync(RegistrationRequestDto registrationRequestDto)
    {
        ApplicationUser user = new ApplicationUser()
        {
            UserName = registrationRequestDto.Email,
            Email = registrationRequestDto.Email,
            NormalizedEmail = registrationRequestDto.Email.ToUpper(),
            Name = registrationRequestDto.Name,
            PhoneNumber = registrationRequestDto.PhoneNumber,
        };

        try
        {
            var result = await userManager.CreateAsync(user, registrationRequestDto.Password);

            if (result.Succeeded)
            {
                var userToReturn = dbContext.ApplicationUsers.First(u => u.UserName == registrationRequestDto.Email);

                UserDto userDto = new UserDto()
                {
                    Email = userToReturn.Email,
                    Id = userToReturn.Id,
                    Name = userToReturn.Name,
                    PhoneNumber = userToReturn.PhoneNumber
                };

                return "User registered successfully";
            }
            else
            {
                return result.Errors.FirstOrDefault().Description;
            }
        }
        catch (Exception ex)
        {
            return "Error encountered";
        }
    }
}
