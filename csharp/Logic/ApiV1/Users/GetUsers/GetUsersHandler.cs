using Todo.Logic.DomainObjects.Constants;
using Todo.Logic.DomainObjects.Entities;
using Todo.Logic.Interfaces;

namespace Todo.Logic.ApiV1.Users;

public class GetUsersHandler : IHandler<GetUsersRequest, GetUsersResponse>
{
    public readonly ILogger<GetUsersHandler> _logger;
    public readonly IRepository<UserEntity> _userRepository;

    public GetUsersHandler(
        ILogger<GetUsersHandler> logger,
        IRepository<UserEntity> userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task<GetUsersResponse> Handle(GetUsersRequest request)
    {
        try
        {
            IList<UserDto> users = string.IsNullOrEmpty(request.Email) ? await GetUsers() : await GetSingleUser(request);
            return new GetUsersResponse
            {
                IsSuccess = true,
                Users = users
            };

        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error getting users");
            return new GetUsersResponse
            {
                IsSuccess = false,
                ErrorMessage = ex.Message,
                ErrorReason = GeneralReasons.InternalError
            };
        }
    }

    private async Task<IList<UserDto>> GetSingleUser(GetUsersRequest request)
    {
        var users = new List<UserDto>();

        var user = await _userRepository.GetFirstByQueryAsync(user => user.Email == request.Email, "");
        if(user != null)
        {
            users.Add(new UserDto
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            });
        }

        return users;
    }

    private async Task<IList<UserDto>> GetUsers()
    {
        var users = await _userRepository.GetAsync(partitionKey: "");
        return users.Select(user => new UserDto
        {
            UserId = user.UserId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        }).ToList();
    }
}