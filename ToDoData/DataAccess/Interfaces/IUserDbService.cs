using ToDoData.Dtos;
using ToDoData.Responses;

namespace ToDoData.DataAccess.Interfaces;

public interface IUserDbService
{
    Task<LoginResponse> LoginUser(LoginDto user);
    Task RegisterUser(RegisterDto userDto);
}
