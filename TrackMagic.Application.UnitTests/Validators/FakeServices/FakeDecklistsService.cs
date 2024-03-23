using System.Linq.Expressions;
using TrackMagic.Application.Features.Decklists;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.UnitTests.Validators.FakeServices
{
    public class FakeDecklistsService : IDecklistsService
    {
        /// <summary>
        /// Fake entries representing Decklist entities in the db.
        /// Only the Id is validated against.
        /// </summary>
        private readonly IQueryable<Decklist> _fakeDecklists = new List<Decklist>
        {
            new Decklist { Id = 1 }
        }.AsQueryable();

        public Task<bool> ExistsAsync(Expression<Func<Decklist, bool>> expression, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_fakeDecklists.Any(expression));
        }
    }
}
