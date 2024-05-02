using AutoMapper;
using TrackMagic.Application.Dtos.Base;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Enums;

namespace TrackMagic.Application.Dtos
{
    public class DetailedGameDto : IDto
    {
        public int Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public int LengthInCycles { get; set; }
        public GameTypes GameType { get; set; }
        public List<ContestantDetails> Contestants { get; set; } = new List<ContestantDetails>();

        public class DetailedGameProfile : Profile
        {
            public DetailedGameProfile()
            {
                CreateMap<Game, DetailedGameDto>();
                CreateMap<Contestant, ContestantDetails>();
                CreateMap<Player, PlayerDetails>();
                CreateMap<Deck, DeckDetails>();
                CreateMap<DecklistCard, DecklistCardDetails>();
                CreateMap<Card, CardDetails>();
                CreateMap<Decklist, DecklistDetails>();
                CreateMap<ColorIdentity, ColorIdentityDetails>();
            }
        }
    }

    public struct ContestantDetails
    {
        public int Id { get; set; }
        public PlayerDetails Player { get; set; }
        public DeckDetails Deck { get; set; }
    }

    public struct PlayerDetails
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public struct DeckDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PlayerDetails Owner { get; set; }
        public List<CardDetails> Commanders { get; set; }
        public CardDetails? Companion { get; set; }
        public DecklistDetails? Decklist { get; set; }
    }

    public struct DecklistDetails
    {
        public int Id { get; set; }
        public List<DecklistCardDetails> Cards { get; set; }
    }

    public struct DecklistCardDetails
    {
        public int Amount { get; set; }
        public CardDetails Card { get; set; }
    }

    public struct CardDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CardTypes> CardTypes { get; set; }
        public ColorIdentityDetails ColorIdentity { get; set; }
    }

    public struct ColorIdentityDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Colors> Colors { get; set; }
    }
}
