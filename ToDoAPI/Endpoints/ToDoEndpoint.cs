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
        todos.MapGet("/task", GetTaskById);

        todos.MapPost("", AddTodo);

        todos.MapPut("", EditTodo);

        todos.MapDelete("", DeleteTodo);
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

    private static async Task<Results<Ok<ToDoDto>, BadRequest>> GetTaskById([FromQuery] int todoId, IToDoDbService data)
    {
        try
        {
            var output = await data.GetTaskById(todoId);
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
    private static async Task<Results<Ok, BadRequest>> AddTodo([FromBody] ToDoDto todo, IToDoDbService data)
    {
        try
        {
            await data.AddTodo(todo);
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
    private static async Task<Results<Ok, BadRequest>> EditTodo([FromBody] ToDoDto todo, IToDoDbService data)
    {
        try
        {
            await data.EditTodo(todo);
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
    private static async Task<Results<Ok, BadRequest>> DeleteTodo([FromQuery] int todoId, IToDoDbService data)
    {
        try
        {
            await data.DeleteTodo(todoId);
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
