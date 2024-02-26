using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TrackMagic.Application.Common.Persistence;

namespace TrackMagic.Application
{
    public static class _Configure
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config, params Assembly[] mediatrAssemblies)
        {
            // Add services.

            return services
                .AddValidatorsFromAssembly(typeof(IAppDbContext).Assembly)
                .AddMediatR(o => o.RegisterServicesFromAssembly(typeof(IAppDbContext).Assembly));
        }
    }
}
