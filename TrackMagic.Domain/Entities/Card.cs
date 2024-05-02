using TrackMagic.Domain.Entities.Base;
using TrackMagic.Shared.Enums;

namespace TrackMagic.Domain.Entities
{
    public class Card : BaseEntity
    {
        public string Name { get; set; } = default!;
        public List<CardTypes> CardTypes { get; set; } = new List<CardTypes>();

        // Relational
        public int ColorIdentityId { get; set; }
        public ColorIdentity ColorIdentity { get; set; } = default!;

        public List<Deck>? CommanderOf { get; set; } = new List<Deck>();
        public List<Deck>? CompanionOf { get; set; } = new List<Deck>();

        public List<DecklistCard>? Usage { get; set; } = new List<DecklistCard>();
    }
}
