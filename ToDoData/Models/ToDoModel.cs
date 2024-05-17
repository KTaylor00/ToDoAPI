namespace ToDoData.Models;

public class ToDoModel
{
    public int ToDoId { get; set; }
    public string? Task { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public bool Is_Completed { get; set; }
    public bool Is_Deleted { get; set; }
}
