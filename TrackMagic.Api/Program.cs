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
        private static ILogger _logger = null!;

        public async static Task Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                AddServices(builder);

                var app = builder.Build();

                _logger = app.Services.GetService<ILogger<Program>>()!;

                UseServices(app, builder.Configuration);

                _logger.LogInformation("Starting application.");

                await app.RunAsync();
            }
            catch (Exception ex) when (!ex.GetType().Name.Equals("StopTheHostException", StringComparison.Ordinal))
            {
                _logger.LogError(ex.Message);
            }
            finally
            {
                _logger.LogInformation("Shutting Down.");
            }
        }

        private static void AddServices(WebApplicationBuilder builder)
        {
            builder.AddConfigurations();
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplication(builder.Configuration, typeof(AppDbContext).Assembly, typeof(IAppDbContext).Assembly);
            builder.Services.AddControllers();
            builder.Services.AddLogging(opt => opt.AddConsole());
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
