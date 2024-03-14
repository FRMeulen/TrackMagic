using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Services;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.Features.Games
{
    public interface IGamesService : IFeatureService<Game> { }

    public class GamesService : BaseFeatureService<Game>, IGamesService
    {
        public GamesService(IAppDbContext appDbContext) : base(appDbContext) { }
    }
}
