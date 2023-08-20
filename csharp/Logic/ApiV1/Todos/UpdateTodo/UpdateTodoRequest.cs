
using System.ComponentModel.DataAnnotations;

namespace Todo.Logic.ApiV1.Todos;

public class UpdateTodoRequest
{
    [JsonPropertyName("userId")]
    [Required]
    public string UserId { get; set; }

    [JsonPropertyName("todoId")]
    [Required]
    public string TodoId { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("dueDate")]
    public DateTime? DueDate { get; set; }

    [JsonPropertyName("completedAt")]
    public DateTime? CompletedAt { get; set; }
}