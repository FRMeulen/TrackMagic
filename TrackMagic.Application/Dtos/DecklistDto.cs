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
                CreateMap<Decklist, DecklistDto>();
                CreateMap<Decklist, ShallowDto<DecklistDto>>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Id));
            }
        }
    }
}
