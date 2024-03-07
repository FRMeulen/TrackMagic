using TrackMagic.Domain.Entities.Base;
using TrackMagic.Shared.Enums;

namespace TrackMagic.Domain.Entities
{
    public class Game : BaseEntity
    {
        public DateTimeOffset Date { get; set; } = default!;
        public int LengthInCycles { get; set; }
        public GameTypes GameType { get; set; }

        // Relational
        public List<Contestant> Contestants { get; set; } = default!;
    }
}
