using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WhereHasMyTimeGone.API.Infrastructure.Persistence;

public class ApplicationDbContextSeed
{
    private readonly ApplicationDbContext _context;

    private ApplicationDbContextSeed(ApplicationDbContext context)
    {
        _context = context;
    }

    private async Task Migrate()
    {
        await _context.Database.MigrateAsync();
    }

    public static async Task Run(IServiceProvider provider)
    {
        var context = provider.GetRequiredService<ApplicationDbContext>();
        
        var seeder = new ApplicationDbContextSeed(context);
        await seeder.Migrate();
    }
}
