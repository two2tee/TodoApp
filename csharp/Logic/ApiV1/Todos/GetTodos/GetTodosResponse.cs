namespace Todo.Logic.ApiV1.Todos;

public class GetTodosResponse : ApiV1BaseResponse
{
    [JsonPropertyName("todos")]
    public IList<TodoDto> Todos { get; set; } = new List<TodoDto>();
}