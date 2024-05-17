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
    public async Task<ToDoDto> GetTaskById(int id) => throw new NotImplementedException();

    public async Task<List<ToDoDto>> GetTasks()
    {
        var tasks = await context.ToDos.Select(t => new ToDoDto
        {
            Task = t.Task,
            Is_Completed = t.Is_Completed,
            Modified = t.Modified
        }).ToListAsync();

        return tasks;
    }

    public async Task AddTodo(ToDoDto todoDto)
    {
        var todo = new ToDoModel()
        {
            Task = todoDto.Task,
            UserId = userId,
            Created = DateTime.Now,
            Modified = DateTime.Now,
            Is_Completed = todoDto.Is_Completed,
            Is_Deleted = false
        };

        context.ToDos.Add(todo);
        await context.SaveChangesAsync();
    }

    public async Task EditTodo(ToDoDto todoDto) => throw new NotImplementedException();
    public async Task DeleteTodo(int id) => throw new NotImplementedException();
}
