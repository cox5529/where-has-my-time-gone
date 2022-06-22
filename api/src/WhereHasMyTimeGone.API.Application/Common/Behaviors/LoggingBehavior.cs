using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using WhereHasMyTimeGone.API.Application.Common.Interfaces;

namespace WhereHasMyTimeGone.API.Application.Common.Behaviors;

public class LoggingBehavior<TRequest> : IRequestPreProcessor<TRequest>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger _logger;

    public LoggingBehavior(ILogger<TRequest> logger, ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _currentUserService.UserId ?? default;

        _logger.LogInformation("CleanArchitecture Request: {Name} {@UserId} {@Request}", requestName, userId, request);
    }
}
