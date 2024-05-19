using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoData.DataAccess.Interfaces;

namespace ToDoData.DataAccess;

public class TokenDbService(IConfiguration config) : ITokenDbService
{
    private readonly IConfiguration config = config;

    public async Task<string> GenerateAccessToken(int id)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("Auth:Key").Value));

        var signingCrendentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        List<Claim> claims =
        [
            new(JwtRegisteredClaimNames.NameId, id.ToString())
        ];

        // Tokens should be short lived (at least 15min) and have refresh tokens that last long.
        // But for the sake of the demo I am letting it last an hour without refesh tokens.
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
