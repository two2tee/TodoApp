
using System.ComponentModel.DataAnnotations;

namespace Todo.Logic.ApiV1.Todos;

public class CreateTodoRequest
{
    [JsonPropertyName("userId")]
    [Required]
    public string UserId { get; set; }

    [JsonPropertyName("title")]
    [Required]
    public string Title { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("dueDate")]
    public DateTime? DueDate { get; set; }
}