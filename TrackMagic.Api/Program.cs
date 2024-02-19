using TrackMagic.Api.Configurations;
using TrackMagic.Infrastructure;

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

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();

                app.UseAuthorization();


                app.MapControllers();

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
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }
    }
}
