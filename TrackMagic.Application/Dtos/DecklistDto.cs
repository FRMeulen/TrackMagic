using AutoMapper;
using TrackMagic.Application.Dtos.Base;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.Dtos
{
    public class DecklistDto : IDto
    {
        public int Id { get; set; }
        public List<ShallowDto<CardDto>> Cards { get; set; } = default!;
        public ShallowDto<DeckDto> Deck { get; set; } = default!;

        public class DecklistProfile : Profile
        {
            public DecklistProfile()
            {
                CreateMap<Decklist, DecklistDto>()
                    .ForMember(dest => dest.Cards, opt => opt.MapFrom<CardsValueResolver>());
                CreateMap<Decklist, ShallowDto<DecklistDto>>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Id));
            }

            public class CardsValueResolver : IValueResolver<Decklist, DecklistDto, List<ShallowDto<CardDto>>>
            {
                public List<ShallowDto<CardDto>> Resolve(Decklist source, DecklistDto destination, List<ShallowDto<CardDto>> member, ResolutionContext context)
                {
                    var result = new List<ShallowDto<CardDto>>();
                    if (source.Cards == null) return result;

                    source.Cards.ForEach(card =>
                    {
                        for (int i = 0; i < card.Amount; i++)
                        {
                            result.Add(context.Mapper.Map<ShallowDto<CardDto>>(card.Card));
                        }
                    });

                    return result;
                }
            }
        }
    }
}
