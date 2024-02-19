using TrackMagic.Domain.Contracts;

namespace TrackMagic.Domain.Entities.Base
{
    public abstract class BaseEntity : IAggregateRoot
    {
        public int Id { get; set; }
    }
}
