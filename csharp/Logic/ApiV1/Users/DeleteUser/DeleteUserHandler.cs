using Todo.Logic.DomainObjects.Constants;
using Todo.Logic.DomainObjects.Entities;
using Todo.Logic.Interfaces;

namespace Todo.Logic.ApiV1.Users;

public class DeleteUserHandler : IHandler<DeleteUserRequest, DeleteUserResponse>
{
    public readonly ILogger<DeleteUserHandler> _logger;
    public readonly IRepository<UserEntity> _userRepository;

    public DeleteUserHandler(
        ILogger<DeleteUserHandler> logger,
        IRepository<UserEntity> userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
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