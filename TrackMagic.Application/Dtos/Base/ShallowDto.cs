using TrackMagic.Domain.Entities.Base;

namespace TrackMagic.Application.Dtos.Base
{
    public class ShallowDto<TEntity> : IShallowDto where TEntity : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Type { get; set; } = typeof(TEntity).Name;
    }
}
