using NSwag;
using TrackMagic.Api.Configurations;
using TrackMagic.Application;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Infrastructure;
using TrackMagic.Infrastructure.Persistence;
using TrackMagic.Infrastructure.Persistence.Context;

namespace TrackMagic.Api
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                AddServices(builder);

                var app = builder.Build();

                UseServices(app);

                await app.RunAsync();
            }
            catch (Exception ex) when (!ex.GetType().Name.Equals("StopTheHostException", StringComparison.Ordinal))
            {
                // Log Exception.
            }
            finally
            {
                // Log Shutdown.
            }
        }

        private static void AddServices(WebApplicationBuilder builder)
        {
            builder.AddConfigurations();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplication(builder.Configuration, typeof(AppDbContext).Assembly, typeof(IAppDbContext).Assembly);
            builder.Services.AddControllers();
            builder.Services.AddOpenApiDocument(options =>
            {
                options.PostProcess = document =>
                {
                    document.Info = new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "TrackMagic",
                        Description = "An ASP.NET Core Web API for tracking Magic games with my friends.",
                    };
                };
            });
        }

        private static async void UseServices(WebApplication app)
        {
            await app.Services.InitializeDatabaseAsync();

            if (app.Environment.IsDevelopment())
            {
                app.UseOpenApi();
                app.UseSwaggerUi();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
        }
    }
}
