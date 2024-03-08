using AutoMapper;
using TrackMagic.Application.Dtos;
using TrackMagic.Application.Dtos.Base;
using TrackMagic.Domain.Entities;
using TrackMagic.Testing.Shared.Fixtures;

namespace TrackMagic.Application.UnitTests.Dtos
{
    public class CardDtoMappingTest
    {
        private readonly IMapper _mapper;

        public CardDtoMappingTest()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddMaps(typeof(IDto))));
        }

        [Fact]
        public void CardToCardDto()
        {
            // Arrange.
            var card = new Fixture<Card>().Create();

            // Act.
            CardDto dto = _mapper.Map<CardDto>(card);

            // Assert.
            Assert.Equivalent(card.Id, dto.Id);
            Assert.Equivalent(card.Name, dto.Name);
            Assert.Equivalent(card.CardTypes, dto.CardTypes);
            Assert.Equivalent((card.ColorIdentity.Id, card.ColorIdentity.Name), (dto.ColorIdentity.Id, dto.ColorIdentity.Name));
            Assert.Equivalent(card.CommanderOf.Count, dto.CommanderOf.Count);
            Assert.Equivalent(card.UsedIn.Count, dto.UsedIn.Count);
        }

        [Fact]
        public void CardToShallowDto()
        {
            // Arrange.
            var card = new Fixture<Card>().Create();

            // Act.
            ShallowDto<CardDto> dto = _mapper.Map<ShallowDto<CardDto>>(card);

            // Assert.
            Assert.Equivalent(card.Id, dto.Id);
            Assert.Equivalent(card.Name, dto.Name);
        }
    }
}