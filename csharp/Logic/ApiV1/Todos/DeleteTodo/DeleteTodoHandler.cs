using Todo.Logic.DomainObjects.Constants;
using Todo.Logic.DomainObjects.Entities;
using Todo.Logic.Interfaces;

namespace Todo.Logic.ApiV1.Todos;

public class DeleteTodoHandler : IHandler<DeleteTodoRequest, DeleteTodoResponse>
{
    public readonly ILogger<DeleteTodoHandler> _logger;
    public readonly IRepository<TodoEntity> _todoRepository;

    public DeleteTodoHandler(
        ILogger<DeleteTodoHandler> logger,
        IRepository<TodoEntity> todoRepository)
    {
        _logger = logger;
        _todoRepository = todoRepository;
    }

    public async Task<DeleteTodoResponse> Handle(DeleteTodoRequest request)
    {
        var response = new DeleteTodoResponse();
        try
        {
            var todo = await _todoRepository.GetAsync(partitionKey: request.UserId, request.TodoId);
            if (todo == null)
            {
                response.ErrorReason = TodoReasons.TodoNotFound;
                return response;
            }

            await _todoRepository.DeleteAsync(todo);

            response.IsSuccess = true;
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting todo");
            response.ErrorReason = GeneralReasons.InternalError;
            response.ErrorMessage = ex.Message;
            return response;
        }
    }
}