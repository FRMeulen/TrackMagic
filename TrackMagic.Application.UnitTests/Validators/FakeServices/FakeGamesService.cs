using System.Linq.Expressions;
using TrackMagic.Application.Features.Games;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.UnitTests.Validators.FakeServices
{
    public class FakeGamesService : IGamesService
    {
        /// <summary>
        /// Fake entries representing Game entities in the db.
        /// Only the Id is validated against.
        /// </summary>
        private readonly IQueryable<Game> _fakeGames = new List<Game>
        {
            new Game { Id = 1 }
        }.AsQueryable();
        public Task<bool> ExistsAsync(Expression<Func<Game, bool>> expression, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_fakeGames.Any(expression));
        }
    }
}
