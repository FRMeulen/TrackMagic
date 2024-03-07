using TrackMagic.Domain.Entities.Base;

namespace TrackMagic.Domain.Entities
{
    public class Deck : BaseEntity
    {
        public string Name { get; set; } = default!;

        // Relational
        public int OwnerId { get; set; }
        public Player Owner { get; set; } = default!;

        public List<Card> Commanders { get; set; } = default!;

        public int? CompanionId { get; set; }
        public Card? Companion { get; set; } = default!;

        public int DecklistId { get; set; }
        public Decklist Decklist { get; set; } = default!;
        public List<Contestant> PilotedBy { get; set; } = default!;
    }
}
