using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace TrackMagic.Infrastructure.ExceptionHandling
{
    public static class _ConfigureExceptionHandling
    {
        public static IServiceCollection AddExceptionHandlers(this IServiceCollection services)
            => services
                .AddExceptionHandler<NotFoundExceptionHandler>()
                .AddExceptionHandler<BadRequestExceptionHandler>()
                .AddExceptionHandler<ValidationExceptionHandler>()
                .AddExceptionHandler<DefaultExceptionHandler>()
                .AddProblemDetails();

        public static IApplicationBuilder UseExceptionHandlers(this IApplicationBuilder app)
            => app
                .UseExceptionHandler();
    }
}
