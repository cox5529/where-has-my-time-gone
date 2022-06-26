using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ApplicationException = WhereHasMyTimeGone.API.Application.Common.Exceptions.ApplicationException;

namespace WhereHasMyTimeGone.API.Application.Common.Behaviors;

public class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public UnhandledExceptionBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;
            if (ex is not ApplicationException)
            {
                _logger.LogError(ex, "Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);
            }
            else
            {
                _logger.LogInformation($"Request: Handled Exception for Request {requestName} {ex.Message}");
            }

            throw;
        }
    }
}
