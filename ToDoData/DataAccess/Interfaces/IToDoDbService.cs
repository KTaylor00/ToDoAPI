using ToDoData.Dtos;

namespace ToDoData.DataAccess.Interfaces;

public interface IToDoDbService
{
    Task<List<ToDoDto>> GetTasks();
    Task AddTask(ToDoDto todoDto);
    Task UpdateTasks(List<ToDoDto> todoDto);
    Task DeleteTask(int id);
}
