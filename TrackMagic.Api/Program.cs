using System.Reflection;
using TrackMagic.Api.Configurations;
using TrackMagic.Application;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Dtos.Base;
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
            var builder = WebApplication.CreateBuilder(args);

            AddServices(builder);

            var app = builder.Build();

            _logger = app.Services.GetService<ILogger<Program>>()!;

            UseServices(app, builder.Configuration);

            _logger.LogInformation("Starting application.");

            await app.RunAsync(CancellationToken.None);
        }

        private static void AddServices(WebApplicationBuilder builder)
        {
            builder.AddConfigurations();
            builder.Services.AddAutoMapper(typeof(Program), typeof(IDto));
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddApplication(builder.Configuration, typeof(AppDbContext).Assembly, typeof(IAppDbContext).Assembly);
            builder.Services.AddControllers().AddApplicationPart(Assembly.GetExecutingAssembly());
        }

        private static async void UseServices(WebApplication app, IConfiguration config)
        {
            app.UseInfrastructure(config);
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
            app.UseAuthorization();
            await app.Services.InitializeDatabaseAsync();
            app.MapControllers();
        }
    }
}
