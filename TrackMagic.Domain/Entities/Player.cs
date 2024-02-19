using TrackMagic.Domain.Entities.Base;

namespace TrackMagic.Domain.Entities
{
    public class Player : BaseEntity
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string FullName => $"{FirstName} {LastName}";
    }
}
