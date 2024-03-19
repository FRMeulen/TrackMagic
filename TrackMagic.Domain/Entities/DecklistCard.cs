using TrackMagic.Domain.Entities.Base;

namespace TrackMagic.Domain.Entities
{
    public class DecklistCard : BaseEntity
    {
        public int Amount { get; set; }

        // Relational.
        public int CardId { get; set; }
        public Card Card { get; set; } = default!;

        public int DecklistId { get; set; }
        public Decklist Decklist { get; set; } = default!;
    }
}
