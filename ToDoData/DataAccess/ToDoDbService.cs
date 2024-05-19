using Microsoft.EntityFrameworkCore;
using ToDoData.DataAccess.Interfaces;
using ToDoData.Dtos;
using ToDoData.Helpers;
using ToDoData.Models;

namespace ToDoData.DataAccess;

public class ToDoDbService(DataContext context, IJWTExtractor extractor) : IToDoDbService
{
    private readonly DataContext context = context;
    private readonly int userId = extractor.GetUserDetailsFromToken();

    public async Task<List<ToDoDto>> GetTasks()
    {
        var userTasks = await context.ToDos
            .Where(t => t.UserId == userId)
            .Select(t => new ToDoDto
            {
                Id = t.Id,
                Task = t.Task,
                Is_Completed = t.Is_Completed,
                Modified = t.Modified
            }).ToListAsync();

        return userTasks;
    }

    public async Task AddTask(ToDoDto todoDto)
    {
        var todo = new ToDoModel()
        {
            Task = todoDto.Task,
            UserId = userId,
            Created = DateTime.Now,
            Modified = DateTime.Now,
            Is_Completed = false
        };

        context.ToDos.Add(todo);
        await context.SaveChangesAsync();
    }

    public async Task UpdateTasks(List<ToDoDto> todoDto)
    {
        var updatedTasks = todoDto
            .Select(t => new ToDoModel
            {
                Id = t.Id,
                UserId = userId,
                Task = t.Task,
                Is_Completed = t.Is_Completed,
                Modified = t.Modified
            }).ToList();

        context.ToDos.UpdateRange(updatedTasks);
        await context.SaveChangesAsync();
    }

    public async Task DeleteTask(int id)
    {
        var todo = new ToDoModel()
        {
            Id = id,
        };

        context.ToDos.Remove(todo);
        await context.SaveChangesAsync();
    }
}
