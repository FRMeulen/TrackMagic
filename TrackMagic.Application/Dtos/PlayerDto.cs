using AutoMapper;
using TrackMagic.Application.Dtos.Base;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.Dtos
{
    public class PlayerDto : IDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public List<ShallowDto<ContestantDto>> Contested { get; set; } = default!;
        public List<ShallowDto<DeckDto>> Decks { get; set; } = default!;

        public class PlayerProfile : Profile
        {
            public PlayerProfile()
            {
                CreateMap<Player, PlayerDto>();
                CreateMap<Player, ShallowDto<PlayerDto>>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FullName));
            }
        }
    }
}
