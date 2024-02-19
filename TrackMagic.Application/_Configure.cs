using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace TrackMagic.Application
{
    public static class _Configure
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config, params Assembly[] mediatrAssemblies)
        {
            // Add services.

            return services;
                //.AddMediatr(o => o.RegisterServicesFromAssemblies(mediatrAssemblies);
        }
    }
}
