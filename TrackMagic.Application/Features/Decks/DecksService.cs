using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Services;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.Features.Decks
{
    public interface IDecksService : IFeatureService<Deck> { }

    public class DecksService : BaseFeatureService<Deck>, IDecksService
    {
        public DecksService(IAppDbContext dbContext) : base(dbContext) { }
    }
}
