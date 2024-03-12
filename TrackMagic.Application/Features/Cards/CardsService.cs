using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Services;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.Features.Cards
{
    public interface ICardsService : IFeatureService<Card> { }

    public class CardsService : BaseFeatureService<Card>, ICardsService
    {
        public CardsService(IAppDbContext dbContext) : base(dbContext) { }
    }
}
