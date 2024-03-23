using TrackMagic.Application.Dtos;
using TrackMagic.Application.Dtos.Base;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.UnitTests.Dtos.Tests
{
    public class CardDtoMappingTests : DtoMappingTestBase
    {
        [Fact]
        public void CardToCardDto()
        {
            // Arrange.
            var card = FixtureFactory.Create<Card>();

            // Act.
            CardDto dto = Mapper.Map<CardDto>(card);

            // Assert.
            Assert.Equal(card.Id, dto.Id);
            Assert.Equal(card.Name, dto.Name);
            Assert.Equal(card.CardTypes, dto.CardTypes);
            Assert.Equal((card.ColorIdentity.Id, card.ColorIdentity.Name), (dto.ColorIdentity.Id, dto.ColorIdentity.Name));
            Assert.Equal(card.CommanderOf.Count, dto.CommanderOf.Count);
            Assert.Equal(card.Usage.Count, dto.Usage.Count);
        }

        [Fact]
        public void CardToShallowDto()
        {
            // Arrange.
            var card = FixtureFactory.Create<Card>();

            // Act.
            ShallowDto<CardDto> dto = Mapper.Map<ShallowDto<CardDto>>(card);

            // Assert.
            Assert.Equal(card.Id, dto.Id);
            Assert.Equal(card.Name, dto.Name);
        }
    }
}