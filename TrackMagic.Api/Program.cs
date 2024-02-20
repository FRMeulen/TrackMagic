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

                UseServices(app, builder.Configuration);

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
        }

        private static async void UseServices(WebApplication app, IConfiguration config)
        {
            app.UseInfrastructure(config);
            await app.Services.InitializeDatabaseAsync();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
        }
    }
}
