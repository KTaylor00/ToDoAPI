using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace ToDoData.Helpers;

public class JWTExtractor(IHttpContextAccessor accessor) : IJWTExtractor
{
    private readonly IHttpContextAccessor accessor = accessor;

    public int GetUserDetailsFromToken()
    {
        string? tokenStr = accessor.HttpContext?.Request.Cookies["session"];

        var handler = new JwtSecurityTokenHandler();

        var token = handler.ReadJwtToken(tokenStr);
        string? userId = token.Payload["Id"].ToString();

        return Convert.ToInt32(userId);
    }
}
