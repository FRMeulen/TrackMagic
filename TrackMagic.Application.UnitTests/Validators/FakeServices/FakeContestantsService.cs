using System.Linq.Expressions;
using TrackMagic.Application.Features.Contestants;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.UnitTests.Validators.FakeServices
{
    public class FakeContestantsService : IContestantsService
    {
        /// <summary>
        /// Fake entries representing Contestant entities in the db.
        /// Only the Id is validated against.
        /// </summary>
        private readonly IQueryable<Contestant> _contestants = new List<Contestant>
        {
            new Contestant { Id = 1 },
            new Contestant { Id = 2 },
            new Contestant { Id = 3 },
            new Contestant { Id = 4 }
        }.AsQueryable();

        public Task<bool> ExistsAsync(Expression<Func<Contestant, bool>> expression, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_contestants.Any(expression));
        }
    }
}
