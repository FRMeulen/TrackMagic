using TrackMagic.Application.Dtos.Base;
using TrackMagic.Shared.Enums;

namespace TrackMagic.Application.Dtos
{
    public class ColorIdentityDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public List<Colors> Colors { get; set; } = default!;
        public List<ShallowDto<CardDto>> CardsInIdentity { get; set; } = default!;
    }
}
