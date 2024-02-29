using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrackMagic.Infrastructure.ExceptionHandling;
using TrackMagic.Infrastructure.OpenApi;
using TrackMagic.Infrastructure.Persistence;

namespace TrackMagic.Infrastructure
{
    public static class _Configure
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
            => services
                .AddExceptionHandlers()
                .AddOpenApiServices(config)
                .AddPersistence(config);

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder, IConfiguration config)
            => builder
                .UseExceptionHandlers()
                .UseOpenApiServices(config);
    }
}
