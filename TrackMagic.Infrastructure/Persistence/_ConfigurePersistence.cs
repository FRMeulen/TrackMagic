using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Infrastructure.Persistence.Context;
using TrackMagic.Infrastructure.Persistence.Initialization;

namespace TrackMagic.Infrastructure.Persistence
{
    public static class _ConfigurePersistence
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("App");

            if (string.IsNullOrEmpty(connectionString)) throw new InvalidOperationException("Database connection string is not configured.");

            return services
                .AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlServer(connectionString);

                    if (config["Environment"] == "Development")
                    {
                        options.EnableSensitiveDataLogging();
                    }
                })
                .AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>())
                .AddTransient<DatabaseInitializer>();
        }

        public static async Task InitializeDatabaseAsync(this IServiceProvider services, CancellationToken cancellationToken = default)
        {
            using var scope = services.CreateScope();

            await scope.ServiceProvider.GetRequiredService<DatabaseInitializer>()
                .InitializeAsync(cancellationToken);
        }
    }
}
