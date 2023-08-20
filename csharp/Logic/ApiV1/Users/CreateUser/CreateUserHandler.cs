using Todo.Logic.DomainObjects.Constants;
using Todo.Logic.DomainObjects.Entities;
using Todo.Logic.Interfaces;

namespace Todo.Logic.ApiV1.Users;

public class CreateUserHandler : IHandler<CreateUserRequest, CreateUserResponse>
{
    public readonly ILogger<CreateUserHandler> _logger;
    public readonly IRepository<UserEntity> _userRepository;

    public CreateUserHandler(
        ILogger<CreateUserHandler> logger,
        IRepository<UserEntity> userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task<CreateUserResponse> Handle(CreateUserRequest request)
    {
        var response = new CreateUserResponse();


        try
        {
            var user = await _userRepository.GetFirstByQueryAsync(user => user.Email == request.Email, "");
            if (user != null)
            {
                response.ErrorReason = UserReasons.UserAlreadyCreated;
                return response;
            }

            await CreateUser(request);

            response.IsSuccess = true;
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating user");
            response.ErrorReason = GeneralReasons.InternalError;
            response.ErrorMessage = ex.Message;
            return response;
        }
    }


    private async Task CreateUser(CreateUserRequest request)
    {
        var user = new UserEntity
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email
        };
        await _userRepository.AddAsync(user);
    }
}