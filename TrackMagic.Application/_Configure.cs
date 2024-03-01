using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TrackMagic.Application.Behaviors;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Features.Players;

namespace TrackMagic.Application
{
    public static class _Configure
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config, params Assembly[] mediatrAssemblies)
        {
            services.AddTransient<IPlayersService, PlayersService>();

            return services
                .AddValidatorsFromAssembly(typeof(IAppDbContext).Assembly)
                .AddMediatR(o =>
                {
                    o.RegisterServicesFromAssemblies(mediatrAssemblies);
                    o.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
                });
        }
    }
}
