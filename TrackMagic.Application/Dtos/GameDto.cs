using AutoMapper;
using TrackMagic.Application.Dtos.Base;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Enums;

namespace TrackMagic.Application.Dtos
{
    public class GameDto : IDto
    {
        public int Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public int LengthInCycles { get; set; }
        public GameTypes GameType { get; set; }
        public List<ShallowDto<ContestantDto>> Contestants { get; set; } = default!;

        public class GameProfile : Profile
        {
            public GameProfile()
            {
                CreateMap<Game, GameDto>();
                CreateMap<Game, ShallowDto<GameDto>>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Id));
            }
        }
    }
}
