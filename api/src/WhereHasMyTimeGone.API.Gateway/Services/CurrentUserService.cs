using System.Security.Claims;
using WhereHasMyTimeGone.API.Application.Common.Interfaces;

namespace WhereHasMyTimeGone.API.Gateway.Services;

public class CurrentUserService : ICurrentUserService
{
    private string? _userId;
    private string? _email;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? UserId
    {
        get
        {
            var contextId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return string.IsNullOrEmpty(contextId) ? _userId : contextId;
        }

        set => _userId = value;
    }

    public string? Email
    {
        get
        {
            var contextId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);
            return string.IsNullOrEmpty(contextId) ? _email : contextId;
        }

        set => _email = value;
    }
}
