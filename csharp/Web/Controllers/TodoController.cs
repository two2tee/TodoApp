using Todo.Logic.ApiV1.Todos;
using Todo.Logic.Interfaces;

namespace Todo.Web.Controllers;

[Route("api/[controller]")]
public class TodosController : Controller
{
    private readonly ILogger<TodosController> _logger;
    private readonly IHandler<CreateTodoRequest, CreateTodoResponse> _createTodoHandler;
    private readonly IHandler<GetTodosRequest, GetTodosResponse> _getTodosHandler;
    private readonly IHandler<DeleteTodoRequest, DeleteTodoResponse> _deleteTodoHandler;
    private readonly IHandler<UpdateTodoRequest, UpdateTodoResponse> _updateTodoHandler;

    public TodosController(
        ILogger<TodosController> logger,
        IHandler<CreateTodoRequest, CreateTodoResponse> createTodoHandler,
        IHandler<GetTodosRequest, GetTodosResponse> getTodosHandler,
        IHandler<DeleteTodoRequest, DeleteTodoResponse> deleteTodoHandler,
        IHandler<UpdateTodoRequest, UpdateTodoResponse> updateTodoHandler)
    {
        _logger = logger;
        _createTodoHandler = createTodoHandler;
        _getTodosHandler = getTodosHandler;
        _deleteTodoHandler = deleteTodoHandler;
        _updateTodoHandler = updateTodoHandler;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTodo([FromBody] CreateTodoRequest request)
    {
        CreateTodoResponse response = await _createTodoHandler.Handle(request);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetTodos([FromQuery] GetTodosRequest request)
    {
        GetTodosResponse response = await _getTodosHandler.Handle(request);
        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteTodo([FromQuery] DeleteTodoRequest request)
    {
        DeleteTodoResponse response = await _deleteTodoHandler.Handle(request);
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTodo([FromBody] UpdateTodoRequest request)
    {
        UpdateTodoResponse response = await _updateTodoHandler.Handle(request);
        return Ok(response);
    }
}