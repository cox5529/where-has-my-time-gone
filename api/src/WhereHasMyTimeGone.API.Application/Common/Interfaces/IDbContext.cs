using Microsoft.EntityFrameworkCore;

namespace WhereHasMyTimeGone.API.Application.Common.Interfaces;

public interface IDbContext
{
    DbSet<T> Set<T>() where T : class;

    Task<int> SaveChangesAsync(CancellationToken cancel = default);
}
