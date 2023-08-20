using Todo.Logic.DomainObjects.Entities;
using Todo.Logic.Interfaces;

namespace Todo.Logic.Services;
public class CleanupService : ICleanupService
{
    private readonly ILogger<CleanupService> _logger;
    private readonly IRepository<TodoEntity> _todoRepository;

    public CleanupService(
        ILogger<CleanupService> logger,
        IRepository<TodoEntity> todoRepository)
    {
        _logger = logger;
        _todoRepository = todoRepository;
    }

    public async Task<bool> CleanupUserReferencesAsync(string userId)
    {

        bool[] isAllCleaned = await Task.WhenAll(DeleteTodosAsync(userId));
        return isAllCleaned.All(isCleaned => isCleaned);
    }


    private async Task<bool> DeleteTodosAsync(string userId)
    {
        try
        {
            var todos = await _todoRepository.GetAsync(partitionKey: userId);
            foreach (var todo in todos)
            {
                await _todoRepository.DeleteAsync(todo);
            }
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting todos for user {userId}", userId);
            return false;
        }
    }
}