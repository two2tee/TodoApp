
using System.ComponentModel.DataAnnotations;

namespace Todo.Logic.ApiV1.Todos;

public class DeleteTodoRequest
{
    [JsonPropertyName("userId")]
    [Required]
    public string UserId { get; set; }

    [JsonPropertyName("todoId")]
    [Required]
    public string TodoId { get; set; }
}