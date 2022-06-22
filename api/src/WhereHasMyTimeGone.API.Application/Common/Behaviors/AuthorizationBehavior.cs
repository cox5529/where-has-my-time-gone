using System.Reflection;
using MediatR;
using WhereHasMyTimeGone.API.Application.Common.Interfaces;
using WhereHasMyTimeGone.API.Application.Common.Security;

namespace WhereHasMyTimeGone.API.Application.Common.Behaviors;

public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ICurrentProfileService _currentProfileService;

    public AuthorizationBehavior(ICurrentUserService currentUserService, ICurrentProfileService currentProfileService)
    {
        _currentProfileService = currentProfileService;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>().ToList();

        if (!authorizeAttributes.Any())
        {
            return await next();
        }

        var profile = await _currentProfileService.GetCurrentProfileAsync(cancellationToken);
        if (profile == null)
        {
            throw new UnauthorizedAccessException();
        }

        return await next();
    }
}
