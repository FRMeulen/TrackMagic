using TrackMagic.Domain.Entities.Base;

namespace TrackMagic.Domain.Entities
{
    public class Decklist : BaseEntity
    {
        // Relational
        public List<Card> Cards { get; set; } = default!;
        public int DeckId { get; set; }
        public virtual Deck Deck { get; set; } = default!;
    }
}
