using AutoMapper;
using TrackMagic.Application.Dtos.Base;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.Dtos
{
    public class DeckDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public ShallowDto<PlayerDto> Owner { get; set; } = default!;
        public List<ShallowDto<CardDto>> Commanders { get; set; } = default!;
        public ShallowDto<CardDto>? Companion { get; set; } = default!;
        public ShallowDto<DecklistDto> Decklist { get; set; } = default!;
        public List<ShallowDto<ContestantDto>> PilotedBy { get; set; } = default!;

        public class DeckProfile : Profile
        {
            public DeckProfile()
            {
                CreateMap<Deck, DeckDto>();
                CreateMap<Deck, ShallowDto<DeckDto>>();
            }
        }
    }
}
