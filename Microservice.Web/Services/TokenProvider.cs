using Microservice.Web.Services.Interfaces;
using Microservice.Web.Utility;

namespace Microservice.Web.Services;

public class TokenProvider(IHttpContextAccessor httpContextAccessor) : ITokenProvider
{
    public void ClearToken()
    {
        httpContextAccessor.HttpContext?.Response.Cookies.Delete(SD.TokenCookie);
    }

    public string? GetToken()
    {
        string? token = null;

        var hasToken = httpContextAccessor.HttpContext?.Request.Cookies.TryGetValue(SD.TokenCookie, out token);

        return hasToken is true ? token : null;
    }

    public void SetToken(string token)
    {
        httpContextAccessor.HttpContext?.Response.Cookies.Append(SD.TokenCookie, token);
    }
}
