using Microsoft.AspNetCore.Mvc;
using Todo.Logic.ApiV1.Users;
using Todo.Logic.Interfaces;

namespace Todo.Web.Controllers;

[Route("api/[controller]")]
public class UsersController : Controller
{
    private readonly ILogger<UsersController> _logger;
    private readonly IHandler<CreateUserRequest, CreateUserResponse> _createUserHandler;
    private readonly IHandler<GetUsersRequest, GetUsersResponse> _getUsersHandler;

    public UsersController(
        ILogger<UsersController> logger,
        IHandler<CreateUserRequest, CreateUserResponse> createUserHandler,
        IHandler<GetUsersRequest, GetUsersResponse> getUsersHandler)
    {
        _logger = logger;
        _createUserHandler = createUserHandler;
        _getUsersHandler = getUsersHandler;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        CreateUserResponse response = await _createUserHandler.Handle(request);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] GetUsersRequest request)
    {
        GetUsersResponse response = await _getUsersHandler.Handle(request);
        return Ok(response);
    }
}