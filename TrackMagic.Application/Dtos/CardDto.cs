using AutoMapper;
using TrackMagic.Application.Dtos.Base;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Enums;

namespace TrackMagic.Application.Dtos
{
    public class CardDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public List<CardTypes> CardTypes { get; set; } = default!;
        public ShallowDto<ColorIdentityDto> ColorIdentity { get; set; } = default!;
        public List<ShallowDto<DeckDto>>? CommanderOf { get; set; } = default!;
        public List<ShallowDto<DeckDto>>? CompanionOf { get; set; } = default!;
        public List<ShallowDto<DecklistDto>> UsedIn { get; set; } = default!;

        public class CardProfile : Profile
        {
            public CardProfile()
            {
                CreateMap<Card, CardDto>();
                CreateMap<Card, ShallowDto<CardDto>>();
            }
        }
    }
}
