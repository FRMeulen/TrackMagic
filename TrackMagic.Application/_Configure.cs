using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TrackMagic.Application.Behaviors;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Features.Cards;
using TrackMagic.Application.Features.ColorIdentities;
using TrackMagic.Application.Features.Contestants;
using TrackMagic.Application.Features.Decklists;
using TrackMagic.Application.Features.Decks;
using TrackMagic.Application.Features.Games;
using TrackMagic.Application.Features.Players;

namespace TrackMagic.Application
{
    public static class _Configure
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config, params Assembly[] mediatrAssemblies)
        {
            services.AddTransient<ICardsService, CardsService>();
            services.AddTransient<IColorIdentitiesService, ColorIdentitiesService>();
            services.AddTransient<IContestantsService, ContestantsService>();
            services.AddTransient<IDecklistsService, DecklistsService>();
            services.AddTransient<IDecksService, DecksService>();
            services.AddTransient<IGamesService, GamesService>();
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
