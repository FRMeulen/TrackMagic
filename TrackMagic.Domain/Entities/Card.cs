using TrackMagic.Domain.Entities.Base;
using TrackMagic.Shared.Enums;

namespace TrackMagic.Domain.Entities
{
    public class Card : BaseEntity
    {
        public string Name { get; set; } = default!;
        public List<CardTypes> CardTypes { get; set; } = default!;

        // Relational
        public int ColorIdentityId { get; set; }
        public ColorIdentity ColorIdentity { get; set; } = default!;

        public List<Deck>? CommanderOf { get; set; } = default!;
        public List<Deck>? CompanionOf { get; set; } = default!;

        public List<DecklistCard>? Usage { get; set; } = default!;
    }
}
