namespace ToDoData.Dtos;

public class ToDoDto
{
    public string? Task { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public bool Is_Completed { get; set; }
}
