using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace TrackMagic.Infrastructure.Middleware
{
    public static class _ConfigureMiddleware
    {
        public static IServiceCollection AddMiddlewareServices(this IServiceCollection services)
            => services.AddScoped<ExceptionHandlingMiddleware>();

        public static IApplicationBuilder UseMiddlewareServices(this IApplicationBuilder app)
            => app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
