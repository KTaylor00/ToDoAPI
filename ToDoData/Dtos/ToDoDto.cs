namespace ToDoData.Dtos;

public class ToDoDto
{
    public int Id { get; set; }
    public string? Task { get; set; }
    public bool Is_Completed { get; set; }
    public DateTime Modified { get; set; }
}
