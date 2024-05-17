namespace ToDoData.DataAccess.Interfaces;

public interface ITokenDbService
{
    Task<string> GenerateAccessToken(int userId);
}
