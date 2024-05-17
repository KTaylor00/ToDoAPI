using ToDoData.Dtos;

namespace ToDoData.DataAccess.Interfaces;

public interface IToDoDbService
{
    Task<List<ToDoDto>> GetTasks();
    Task<ToDoDto> GetTaskById(int id);
    Task AddTodo(ToDoDto todoDto);
    Task EditTodo(ToDoDto todoDto);
    Task DeleteTodo(int id);
}
