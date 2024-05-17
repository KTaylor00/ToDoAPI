using Microsoft.AspNetCore.Http;
using ToDoData.DataAccess.Interfaces;
using ToDoData.Dtos;
using ToDoData.Helpers;
using ToDoData.Models;
using ToDoData.Responses;

namespace ToDoData.DataAccess;

public class UserDbService(DataContext context, ITokenDbService tokenDbService, IHttpContextAccessor accessor) : IUserDbService
{
    private readonly DataContext context = context;
    private readonly ITokenDbService tokenDbService = tokenDbService;
    private readonly IHttpContextAccessor accessor = accessor;

    public async Task RegisterUser(RegisterDto userDto)
    {
        var hashes = Hasher.Hash(userDto.Password);
        string? passwordHash = hashes.Item1;
        string? salt = hashes.Item2;

        var user = new UserModel()
        {
            Username = userDto.Username,
            Name = userDto.Name,
            Surname = userDto.Surname,
            Password = passwordHash,
            PasswordSalt = salt,
            Created = DateTime.Now,
            Modified = DateTime.Now,
            Is_Deleted = false
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();
    }

    public async Task<LoginResponse> LoginUser(LoginDto loginDto)
    {
        var user = context.Users.Single(u => u.Username == loginDto.Username);

        if (!Hasher.VerifyHash(loginDto.Password, user?.Password, user?.PasswordSalt))
        {
            return new LoginResponse
            {
                Success = false,
                Error = "Incorrect login details."
            };
        }

        var token = await tokenDbService.GenerateAccessToken(user.Id);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTimeOffset.UtcNow.AddHours(1),
            Secure = true,
            IsEssential = true,
            SameSite = SameSiteMode.None,
        };
        accessor.HttpContext?.Response.Cookies.Append("session", token, cookieOptions);

        LoginResponse response = new()
        {
            User = new UserDto
            {
                Username = user.Username,
                Name = user.Name,
                Surname = user.Surname,
            }
        };

        return response;
    }
}
