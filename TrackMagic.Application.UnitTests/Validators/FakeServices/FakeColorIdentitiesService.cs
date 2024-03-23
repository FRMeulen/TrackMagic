using System.Linq.Expressions;
using TrackMagic.Application.Features.ColorIdentities;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.UnitTests.Validators.FakeServices
{
    public class FakeColorIdentitiesService : IColorIdentitiesService
    {
        /// <summary>
        /// Fake entries representing ColorIdentity entities in the db.
        /// Only the Id and Name are validated against.
        /// </summary>
        private readonly IQueryable<ColorIdentity> _fakeColorIdentities = new List<ColorIdentity>
        {
            new ColorIdentity { Id = 1, Name = "Simic" },
            new ColorIdentity { Id = 2, Name = "Mardu" }
        }.AsQueryable();

        public Task<bool> ExistsAsync(Expression<Func<ColorIdentity, bool>> expression, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_fakeColorIdentities.Any(expression));
        }
    }
}
