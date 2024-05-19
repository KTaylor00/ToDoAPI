using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ToDoData.Helpers;

public class JWTExtractor(IHttpContextAccessor accessor) : IJWTExtractor
{
    private readonly IHttpContextAccessor accessor = accessor;

    public int GetUserDetailsFromToken()
    {
        string? userId = accessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        return Convert.ToInt32(userId);
    }
}
