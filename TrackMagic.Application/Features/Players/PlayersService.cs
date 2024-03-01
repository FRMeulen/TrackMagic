using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Services;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.Features.Players
{
    public interface IPlayersService : IFeatureService<Player> { }

    public class PlayersService : BaseFeatureService<Player>, IPlayersService
    {
        public PlayersService(IAppDbContext appDbContext) : base(appDbContext) { }
    }
}
