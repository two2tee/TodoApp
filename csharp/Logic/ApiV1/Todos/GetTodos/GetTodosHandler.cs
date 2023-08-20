using Todo.Logic.DomainObjects.Constants;
using Todo.Logic.DomainObjects.Entities;
using Todo.Logic.Interfaces;

namespace Todo.Logic.ApiV1.Todos;

public class GetTodosHandler : IHandler<GetTodosRequest, GetTodosResponse>
{
    public readonly ILogger<GetTodosHandler> _logger;
    public readonly IRepository<TodoEntity> _todoRepository;

    public GetTodosHandler(
        ILogger<GetTodosHandler> logger,
        IRepository<TodoEntity> todoRepository)
    {
        _logger = logger;
        _todoRepository = todoRepository;
    }

    public async Task<GetTodosResponse> Handle(GetTodosRequest request)
    {
        try
        {
            IList<TodoDto> todos = await GetTodosForUser(request);
            return new GetTodosResponse
            {
                IsSuccess = true,
                Todos = todos
            };

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting todos");
            return new GetTodosResponse
            {
                IsSuccess = false,
                ErrorMessage = ex.Message,
                ErrorReason = GeneralReasons.InternalError
            };
        }
    }

    private async Task<IList<TodoDto>> GetTodosForUser(GetTodosRequest request)
    {
        var todoEntities = await _todoRepository.GetAsync(partitionKey: request.UserId);
        return todoEntities.Select(todoEntity => new TodoDto
        {
            Title = todoEntity.Title,
            Description = todoEntity.Description,
            CompletedAt = todoEntity.CompletedAt,
            DueDate = todoEntity.DueDate,
        }).ToList();
    }
}