using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WhereHasMyTimeGone.API.Application;
using WhereHasMyTimeGone.API.Application.Common.Interfaces;
using WhereHasMyTimeGone.API.Infrastructure.Persistence;
using WhereHasMyTimeGone.API.Infrastructure.Services;

namespace WhereHasMyTimeGone.API.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplication(configuration);
        services.AddDbContext<ApplicationDbContext>(
            options =>
            {
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            });

        services.AddScoped<ICurrentProfileService, CurrentProfileService>();
        services.AddScoped<IDateTime, DateTimeService>();
        services.AddScoped<IDbContext, ApplicationDbContext>();
        services.AddScoped<IStringNormalizer, StringNormalizer>();
        services.AddScoped<ICryptographyService, CryptographyService>();
        
        return services;
    }
}
