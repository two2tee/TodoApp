using Microsoft.Extensions.Configuration;
using Todo.Logic.Interfaces;

namespace Todo.Logic.ApiV1.Status;

public class GetStatusHandler : IHandler<GetStatusRequest, GetStatusResponse>
{
    public readonly ILogger<GetStatusHandler> _logger;
    public readonly IConfiguration _configuration;

    public GetStatusHandler(
        ILogger<GetStatusHandler> logger,
        IConfiguration configuration)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<GetStatusResponse> Handle(GetStatusRequest request)
    {
        return new GetStatusResponse
        {
            Version = _configuration.GetValue<string>("Version"),
            Environment = _configuration.GetValue<string>("ASPNETCORE_ENVIRONMENT"),
        };
    }
}