namespace ToDoData.Models;

public class UserModel
{
    public int UserId { get; set; }
    public string? Username { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Password { get; set; }
    public string? PasswordSalt { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public bool Is_Deleted { get; set; }
}
