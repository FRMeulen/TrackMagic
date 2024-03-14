using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Services;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.Features.Decklists
{
    public interface IDecklistsService : IFeatureService<Decklist> { }
    public class DecklistsService : BaseFeatureService<Decklist>, IDecklistsService
    {
        public DecklistsService(IAppDbContext dbContext) : base(dbContext) { }
    }
}
