using System.Linq.Expressions;
using TrackMagic.Application.Features.Cards;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Enums;

namespace TrackMagic.Application.UnitTests.Validators.FakeServices
{
    public class FakeCardsService : ICardsService
    {
        /// <summary>
        /// Fake entries representing Card entities in the db.
        /// Only Id, Name, and CardTypes are validated against.
        /// </summary>
        private readonly IQueryable<Card> _fakeCards = new List<Card>
        {
            new Card { Id = 1, Name = "Portal to Phyrexia", CardTypes = new List<CardTypes> { CardTypes.Artifact } },
            new Card { Id = 2, Name = "Invasion of Segovia", CardTypes = new List<CardTypes> { CardTypes.Battle } },
            new Card { Id = 3, Name = "Scute Swarm", CardTypes = new List<CardTypes> { CardTypes.Creature } },
            new Card { Id = 4, Name = "Sterling Grove", CardTypes = new List<CardTypes> { CardTypes.Enchantment } },
            new Card { Id = 5, Name = "Lightning Helix", CardTypes = new List<CardTypes> { CardTypes.Instant } },
            new Card { Id = 6, Name = "Command Tower", CardTypes = new List<CardTypes> { CardTypes.Land } },
            new Card { Id = 7, Name = "Chandra, Torch of Defiance", CardTypes = new List<CardTypes> { CardTypes.Planeswalker } },
            new Card { Id = 8, Name = "Time Wipe", CardTypes = new List<CardTypes> { CardTypes.Sorcery } }
        }.AsQueryable();

        public Task<bool> ExistsAsync(Expression<Func<Card, bool>> expression, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_fakeCards.Any(expression));
        }
    }
}
