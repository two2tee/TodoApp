namespace Todo.Logic.ApiV1.Todos;

public class GetTodosRequest
{
    [FromQuery(Name = "userId")]
    public string UserId { get; set; }
}