using Microsoft.AspNetCore.Mvc;
using Todo.Logic.ApiV1.Status;
using Todo.Logic.Interfaces;

namespace Todo.Web.Controllers;

[Route("api/[controller]")]
public class StatusController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly IHandler<GetStatusRequest, GetStatusResponse> _getStatusHandler;


    public StatusController(
        IConfiguration configuration,
        IHandler<GetStatusRequest, GetStatusResponse> getStatusHandler)
    {
        _configuration = configuration;
        _getStatusHandler = getStatusHandler;
    }

    [HttpGet()]
    public IActionResult Get([FromQuery] GetStatusRequest request)
    {
        var response = _getStatusHandler.Handle(request);
        return Ok(response);
    }
}