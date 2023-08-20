
using System.ComponentModel.DataAnnotations;

namespace Todo.Logic.ApiV1.Todos;

public class DeleteTodoRequest
{
    [FromQuery(Name = "userId")]
    [Required]
    public string UserId { get; set; }

    [FromQuery(Name = "todoId")]
    [Required]
    public string TodoId { get; set; }
}