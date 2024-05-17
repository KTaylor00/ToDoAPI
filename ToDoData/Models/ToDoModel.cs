using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoData.Models;

public class ToDoModel
{
    public int Id { get; set; }
    [ForeignKey("User")]
    public int UserId { get; set; }
    public virtual UserModel? User { get; set; }
    public string? Task { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public bool Is_Completed { get; set; }
    public bool Is_Deleted { get; set; }
}
