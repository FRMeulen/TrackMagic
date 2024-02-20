using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using TrackMagic.Shared.Exceptions;

namespace TrackMagic.Infrastructure.OpenApi
{
    public static class _ConfigureOpenApi
    {
        public static IServiceCollection AddOpenApiServices(this IServiceCollection services, IConfiguration config)
        {
            var settings = config.GetSection(nameof(SwaggerSettings)).Get<SwaggerSettings>();
            if (settings == null) throw new ConfigurationException(nameof(SwaggerSettings));
            
            return services
                .AddOpenApiDocument(options =>
                {
                    options.PostProcess = document =>
                    {
                        document.Info = new OpenApiInfo
                        {
                            Version = settings.Version,
                            Title = settings.Title,
                            Description = settings.Description,
                        };
                    };
                }); ;
        }

        public static IApplicationBuilder UseOpenApiServices(this IApplicationBuilder app, IConfiguration config)
        {
            var enabled = config.GetValue<bool>("SwaggerSettings:Enabled");
            if (!enabled) return app;
            
            app.UseOpenApi();
            app.UseSwaggerUi();

            return app;
        }
    }
}
