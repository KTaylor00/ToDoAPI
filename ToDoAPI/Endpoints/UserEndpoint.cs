using Carter;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using ToDoData.DataAccess.Interfaces;
using ToDoData.Dtos;
using ToDoData.Responses;

namespace ToDoAPI.Endpoints;

public class UserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var users = app.MapGroup("/users");

        users.MapPost("/login", LoginUser).AllowAnonymous();
        users.MapPost("/register", RegisterUser).AllowAnonymous();
        users.MapPost("/logout", LogoutUser).AllowAnonymous();
    }

    #region Post Methods
    private static async Task<Results<Ok, BadRequest>> RegisterUser([FromBody] RegisterDto user, IUserDbService data)
    {
        try
        {
            await data.RegisterUser(user);
            return TypedResults.Ok();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "The Post request to /users failed.");
            return TypedResults.BadRequest();
        }
    }

    private static async Task<Results<Ok<LoginResponse>, BadRequest>> LoginUser([FromBody] LoginDto login, IUserDbService data)
    {
        try
        {
            var output = await data.LoginUser(login);
            return TypedResults.Ok(output);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "The Post request to /users failed.");
            return TypedResults.BadRequest();
        }
    }

    private static Results<Ok, BadRequest> LogoutUser(IUserDbService data)
    {
        try
        {
            data.LogoutUser();
            return TypedResults.Ok();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "The Post request to /users failed.");
            return TypedResults.BadRequest();
        }
    }
    #endregion
}
