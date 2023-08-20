using Todo.Logic.DomainObjects.Constants;
using Todo.Logic.DomainObjects.Entities;
using Todo.Logic.Interfaces;

namespace Todo.Logic.ApiV1.Todos;

public class CreateTodoHandler : IHandler<CreateTodoRequest, CreateTodoResponse>
{
    public readonly ILogger<CreateTodoHandler> _logger;
    public readonly IRepository<TodoEntity> _todoRepository;

    public CreateTodoHandler(
        ILogger<CreateTodoHandler> logger,
        IRepository<TodoEntity> todoRepository)
    {
        _logger = logger;
        _todoRepository = todoRepository;
    }

    public async Task<CreateTodoResponse> Handle(CreateTodoRequest request)
    {
        var response = new CreateTodoResponse();

        try
        {
            await CreateTodo(request);

            response.IsSuccess = true;
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating todo");
            response.ErrorReason = GeneralReasons.InternalError;
            response.ErrorMessage = ex.Message;
            return response;
        }
    }


    private async Task CreateTodo(CreateTodoRequest request)
    {
        var todo = new TodoEntity(request.UserId)
        {
            Title = request.Title,
            Description = request.Description,
            DueDate = request.DueDate
        };
        await _todoRepository.AddAsync(todo);
    }
}