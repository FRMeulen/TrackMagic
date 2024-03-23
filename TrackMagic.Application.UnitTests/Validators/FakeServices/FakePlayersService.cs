using System.Linq.Expressions;
using TrackMagic.Application.Features.Players;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.UnitTests.Validators.FakeServices
{
    public class FakePlayersService : IPlayersService
    {
        /// <summary>
        /// Fake entries representing Player entities in the db.
        /// Only the Id, FirstName, and LastName is validated against.
        /// </summary>
        private readonly IQueryable<Player> _fakePlayers = new List<Player>
        {
            new Player { Id = 1, FirstName = "Tony", LastName = "Stark" },
            new Player { Id = 2, FirstName = "Steve", LastName = "Rodgers" },
            new Player { Id = 3, FirstName = "Stephen", LastName = "Strange" }
        }.AsQueryable();

        public Task<bool> ExistsAsync(Expression<Func<Player, bool>> expression, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_fakePlayers.Any(expression));
        }
    }
}
