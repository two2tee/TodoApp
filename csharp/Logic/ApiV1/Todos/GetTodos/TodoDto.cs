namespace Todo.Logic.ApiV1.Todos;

public class TodoDto
{
    [JsonPropertyName("todoId")]
    public string TodoId { get; set; }

    [JsonPropertyName("userId")]
    public string UserId { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("completedAt")]
    public DateTime? CompletedAt { get; set; }

    [JsonPropertyName("dueDate")]
    public DateTime? DueDate { get; set; }
}