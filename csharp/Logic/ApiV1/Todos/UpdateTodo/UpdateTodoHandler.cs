using Todo.Logic.DomainObjects.Constants;
using Todo.Logic.DomainObjects.Entities;
using Todo.Logic.Interfaces;

namespace Todo.Logic.ApiV1.Todos;

public class UpdateTodoHandler : IHandler<UpdateTodoRequest, UpdateTodoResponse>
{
    public readonly ILogger<UpdateTodoHandler> _logger;
    public readonly IRepository<TodoEntity> _todoRepository;

    public UpdateTodoHandler(
        ILogger<UpdateTodoHandler> logger,
        IRepository<TodoEntity> todoRepository)
    {
        _logger = logger;
        _todoRepository = todoRepository;
    }

    public async Task<UpdateTodoResponse> Handle(UpdateTodoRequest request)
    {
        var response = new UpdateTodoResponse();

        try
        {
            TodoEntity todoEntity = await _todoRepository.GetAsync(partitionKey: request.UserId, request.TodoId);
            if (todoEntity == null)
            {
                response.ErrorReason = TodoReasons.TodoNotFound;
                return response;
            }

            await UpdateTodo(request, todoEntity);

            response.IsSuccess = true;
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating todo");
            response.ErrorReason = GeneralReasons.InternalError;
            response.ErrorMessage = ex.Message;
            return response;
        }
    }


    private async Task UpdateTodo(UpdateTodoRequest request, TodoEntity todoEntity)
    {
        todoEntity.Title = request.Title;
        todoEntity.Description = request.Description;
        todoEntity.DueDate = request.DueDate;
        todoEntity.CompletedAt = request.CompletedAt;
        await _todoRepository.UpdateAsync(todoEntity);
    }
}