using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrackMagic.Infrastructure.OpenApi;
using TrackMagic.Infrastructure.Persistence;

namespace TrackMagic.Infrastructure
{
    public static class _Configure
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
            => services
                .AddOpenApiServices(config)
                .AddPersistence(config);

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder, IConfiguration config)
            => builder.UseOpenApiServices(config);
    }
}
