namespace Todo.Logic.Interfaces;
public interface ICleanupService
{
    Task<bool> CleanupUserReferencesAsync(string userId);

}