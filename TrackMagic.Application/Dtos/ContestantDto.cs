using AutoMapper;
using TrackMagic.Application.Dtos.Base;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.Dtos
{
    public class ContestantDto : IDto
    {
        public int Id { get; set; }
        public int Points { get; set; }
        public ShallowDto<GameDto> Game { get; set; } = default!;
        public ShallowDto<PlayerDto> Player { get; set; } = default!;
        public ShallowDto<DeckDto> Deck { get; set; } = default!;

        public class ContestantProfile : Profile
        {
            public ContestantProfile()
            {
                CreateMap<Contestant, ContestantDto>();
                CreateMap<Contestant, ShallowDto<ContestantDto>>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Id));
            }
        }
    }
}
