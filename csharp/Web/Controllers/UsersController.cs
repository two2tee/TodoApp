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
    private readonly IHandler<DeleteUserRequest, DeleteUserResponse> _deleteUserHandler;

    public UsersController(
        ILogger<UsersController> logger,
        IHandler<CreateUserRequest, CreateUserResponse> createUserHandler,
        IHandler<GetUsersRequest, GetUsersResponse> getUsersHandler,
        IHandler<DeleteUserRequest, DeleteUserResponse> deleteUserHandler)
    {
        _logger = logger;
        _createUserHandler = createUserHandler;
        _getUsersHandler = getUsersHandler;
        _deleteUserHandler = deleteUserHandler;
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

    [HttpDelete]
    public async Task<IActionResult> DeleteUser([FromQuery] DeleteUserRequest request)
    {
        DeleteUserResponse response = await _deleteUserHandler.Handle(request);
        return Ok(response);
    }
}