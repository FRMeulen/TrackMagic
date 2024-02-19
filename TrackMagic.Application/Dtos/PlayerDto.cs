using AutoMapper;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.Dtos
{
    public class PlayerDto : IDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;

        public class PlayerProfile : Profile
        {
            public PlayerProfile()
            {
                CreateMap<Player, PlayerDto>();
            }
        }
    }
}
