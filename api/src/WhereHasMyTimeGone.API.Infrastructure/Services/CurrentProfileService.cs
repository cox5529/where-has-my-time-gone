using Microsoft.EntityFrameworkCore;
using WhereHasMyTimeGone.API.Application.Common.Interfaces;
using WhereHasMyTimeGone.API.Domain.Entities;

namespace WhereHasMyTimeGone.API.Infrastructure.Services;

public class CurrentProfileService : ICurrentProfileService
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IDbContext _context;

    public CurrentProfileService(ICurrentUserService currentUserService, IDbContext context)
    {
        _currentUserService = currentUserService;
        _context = context;
    }

    public async Task<UserProfile?> GetCurrentProfileAsync(CancellationToken cancel = default)
    {
        if (_currentUserService.UserId == null)
        {
            return null;
        }

        return await _context.Set<UserProfile>().FirstOrDefaultAsync(x => x.UserId == _currentUserService.UserId, cancel);
    }
}
