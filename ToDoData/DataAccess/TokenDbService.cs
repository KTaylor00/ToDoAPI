using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoData.DataAccess.Interfaces;
using ToDoData.Models;

namespace ToDoData.DataAccess;

public class TokenDbService(IConfiguration config) : ITokenDbService
{
    private readonly IConfiguration config = config;

    public async Task<string> GenerateAccessToken(int userId)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("Auth:Key").Value!));

        var signingCrendentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        List<Claim> claims =
        [
            new(JwtRegisteredClaimNames.NameId, userId.ToString())
        ];

        // Tokens should be short lived (at least 15min) but for the sake of the demo I am letting it last longer.
        var token = new JwtSecurityToken(
            issuer: config.GetSection("Auth:Issuer").Value,
            audience: config.GetSection("Auth:Audience").Value,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: signingCrendentials);

        return await Task.Run(() => new JwtSecurityTokenHandler().WriteToken(token));
    }
}
