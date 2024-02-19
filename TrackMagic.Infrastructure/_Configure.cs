using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrackMagic.Infrastructure.Persistence;

namespace TrackMagic.Infrastructure
{
    public static class _Configure
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
            => services.AddPersistence(config);
    }
}
