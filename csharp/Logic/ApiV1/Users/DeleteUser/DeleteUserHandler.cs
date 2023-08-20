using Todo.Logic.DomainObjects.Constants;
using Todo.Logic.DomainObjects.Entities;
using Todo.Logic.Interfaces;

namespace Todo.Logic.ApiV1.Users;

public class DeleteUserHandler : IHandler<DeleteUserRequest, DeleteUserResponse>
{
    public readonly ILogger<DeleteUserHandler> _logger;
    public readonly IRepository<UserEntity> _userRepository;
    public readonly ICleanupService _cleanupService;

    public DeleteUserHandler(
        ILogger<DeleteUserHandler> logger,
        IRepository<UserEntity> userRepository,
        ICleanupService cleanupService)
    {
        _logger = logger;
        _userRepository = userRepository;
        _cleanupService = cleanupService;
    }

    public async Task<DeleteUserResponse> Handle(DeleteUserRequest request)
    {
        var response = new DeleteUserResponse();
        try
        {
            var user = await _userRepository.GetAsync(partitionKey: "", rowKey: request.UserId);
            if (user == null)
            {
                response.ErrorReason = UserReasons.UserNotFound;
                return response;
            }

            bool isReferencesCleaned = await _cleanupService.CleanupUserReferencesAsync(user.RowKey);

            if (!isReferencesCleaned)
            {
                response.ErrorReason = UserReasons.UserReferencesNotFullyCleaned;
                return response;
            }

            await _userRepository.DeleteAsync(user);

            response.IsSuccess = true;
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting user");
            response.ErrorReason = GeneralReasons.InternalError;
            response.ErrorMessage = ex.Message;
            return response;
        }
    }
}