using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Services;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.Features.Cards
{
    public interface ICardsService : IFeatureService<Card>
    {
        Task<bool> AllExistAsync(List<int> cardIds, CancellationToken cancellationToken);
    }

    public class CardsService : BaseFeatureService<Card>, ICardsService
    {
        public CardsService(IAppDbContext dbContext) : base(dbContext) { }

        public async Task<bool> AllExistAsync(List<int> cardIds, CancellationToken cancellationToken)
        {
            foreach (int id in cardIds)
            {
                if (!await ExistsAsync(c => c.Id == id, cancellationToken)) return false;
            }

            return true;
        }
    }
}
