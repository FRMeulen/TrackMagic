using TrackMagic.Domain.Entities.Base;

namespace TrackMagic.Domain.Entities
{
    public class Contestant : BaseEntity
    {
        public int Points { get; set; } = default!;

        // Relational
        public int GameId { get; set; }
        public virtual Game Game { get; set; } = default!;

        public int PlayerId { get; set; }
        public Player Player { get; set; } = default!;

        public int DeckId { get; set; }
        public Deck Deck { get; set; } = default!;
    }
}
