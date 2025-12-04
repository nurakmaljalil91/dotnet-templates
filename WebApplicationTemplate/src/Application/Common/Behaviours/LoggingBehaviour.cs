using Application.Mediator;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviours;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{

    private readonly ILogger _logger;
    private readonly ICurrentUserService _currentUserService;

    /// <summary>
    /// DEVELOPER NOTES:
    /// We do not use Identity Service since we not implement it from the get go
    /// so we modified this code from clean architecture and just get user id
    /// from jwt. But you can add Identity Service if you implement it
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="currentUserService"></param>
    /// <param name="identityService"></param>
    public LoggingBehavior(ILogger<TRequest> logger, ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _currentUserService.Username ?? string.Empty;
        var start = DateTime.UtcNow;

        _logger.LogInformation("Handling {RequestName}", requestName);

        try
        {
            var response = await next().ConfigureAwait(false);
            var elapsed = (DateTime.UtcNow - start).TotalMilliseconds;
            _logger.LogInformation("Handled {RequestName} in {Elapsed:0.000} ms", requestName, elapsed);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error handling {RequestName}", requestName);
            throw;
        }
    }
}

