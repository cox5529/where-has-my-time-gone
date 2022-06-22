using System.Reflection;
using Microsoft.EntityFrameworkCore;
using WhereHasMyTimeGone.API.Application.Common.Interfaces;

namespace WhereHasMyTimeGone.API.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
