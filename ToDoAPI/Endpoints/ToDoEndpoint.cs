using Carter;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using ToDoData.DataAccess.Interfaces;
using ToDoData.Dtos;

namespace ToDoAPI.Endpoints;

public class ToDoEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var todos = app.MapGroup("/to-dos");

        todos.MapGet("", GetTasks);

        todos.MapPost("", AddTask);

        todos.MapPut("", UpdateTasks);

        todos.MapDelete("", DeleteTask);
    }

    #region Get Methods
    private static async Task<Results<Ok<List<ToDoDto>>, BadRequest>> GetTasks(IToDoDbService data)
    {
        try
        {
            var output = await data.GetTasks();
            return TypedResults.Ok(output);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "The Get request to /to-dos failed.");
            return TypedResults.BadRequest();
        }
    }

    #endregion

    #region Post Methods
    private static async Task<Results<Ok, BadRequest>> AddTask([FromBody] ToDoDto todo, IToDoDbService data)
    {
        try
        {
            await data.AddTask(todo);
            return TypedResults.Ok();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "The Post request to /to-dos failed.");
            return TypedResults.BadRequest();
        }
    }

    #endregion

    #region Put Methods
    private static async Task<Results<Ok, BadRequest>> UpdateTasks([FromBody] List<ToDoDto> todo, IToDoDbService data)
    {
        try
        {
            await data.UpdateTasks(todo);
            return TypedResults.Ok();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "The Put request to /to-dos failed.");
            return TypedResults.BadRequest();
        }
    }

    #endregion

    #region Delete Methods
    private static async Task<Results<Ok, BadRequest>> DeleteTask([FromQuery] int id, IToDoDbService data)
    {
        try
        {
            await data.DeleteTask(id);
            return TypedResults.Ok();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "The Delete request to /to-dos failed.");
            return TypedResults.BadRequest();
        }
    }

    #endregion
}
