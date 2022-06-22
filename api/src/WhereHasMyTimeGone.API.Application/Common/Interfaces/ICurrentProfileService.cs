using WhereHasMyTimeGone.API.Domain.Entities;

namespace WhereHasMyTimeGone.API.Application.Common.Interfaces;

public interface ICurrentProfileService
{
    Task<UserProfile> GetCurrentProfileAsync(CancellationToken cancel = default);
}
