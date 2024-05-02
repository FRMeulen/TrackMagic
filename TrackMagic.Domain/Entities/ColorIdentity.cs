using TrackMagic.Domain.Entities.Base;
using TrackMagic.Shared.Enums;

namespace TrackMagic.Domain.Entities
{
    public class ColorIdentity : BaseEntity
    {
        public string Name { get; set; } = default!;
        public List<Colors> Colors { get; set; } = new List<Colors>();

        // Relational
        public List<Card> CardsInIdentity { get; set; } = default!;
    }
}
