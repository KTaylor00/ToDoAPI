using System.Text.Json.Serialization;

namespace ToDoData.Responses;

public abstract class BaseReponse
{
    [JsonIgnore]
    public bool Success { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Error { get; set; }
}
