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
        public List<ShallowDto<DecklistDto>> Usage { get; set; } = default!;

        public class CardProfile : Profile
        {
            public CardProfile()
            {
                CreateMap<Card, CardDto>()
                    .ForMember(dest => dest.Usage, opt => opt.MapFrom<UsageValueResolver>());
                CreateMap<Card, ShallowDto<CardDto>>();
            }

            public class UsageValueResolver : IValueResolver<Card, CardDto, List<ShallowDto<DecklistDto>>>
            {
                public List<ShallowDto<DecklistDto>> Resolve(Card source, CardDto destination, List<ShallowDto<DecklistDto>> member, ResolutionContext context)
                {
                    return source.Usage == null
                        ? new List<ShallowDto<DecklistDto>>()
                        : source.Usage
                            .Select(dlc => dlc.Decklist)
                            .Select(context.Mapper.Map<ShallowDto<DecklistDto>>).ToList();
                }
            }
        }
    }
}
