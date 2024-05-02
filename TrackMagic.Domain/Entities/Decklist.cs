using TrackMagic.Domain.Entities.Base;

namespace TrackMagic.Domain.Entities
{
    public class Decklist : BaseEntity
    {
        // Relational
        public List<DecklistCard> Cards { get; set; } = new List<DecklistCard>();

        public int? DeckId { get; set; }
        public virtual Deck? Deck { get; set; } = default!;
    }
}
