using TrackMagic.Application.Dtos;
using TrackMagic.Application.Dtos.Base;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.UnitTests.Dtos.Tests
{
    public class DecklistDtoMappingTests : DtoMappingTestBase
    {
        [Fact]
        public void DecklistToDecklistDto()
        {
            // Arrange.
            var decklist = FixtureFactory.Create<Decklist>();

            // Act.
            var dto = Mapper.Map<DecklistDto>(decklist);

            // Assert.
            Assert.Equal(decklist.Id, dto.Id);
            Assert.Equal(decklist.Cards.Count, dto.Cards.Count);
            Assert.Equal(decklist.Deck.Id, dto.Deck.Id);
        }

        [Fact]
        public void DecklistToShallowDto()
        {
            // Arrange.
            var decklist = FixtureFactory.Create<Decklist>();

            // Act.
            var dto = Mapper.Map<ShallowDto<DecklistDto>>(decklist);

            // Assert.
            Assert.Equal(decklist.Id, dto.Id);
            Assert.Equal(decklist.Id.ToString(), dto.Name);
        }
    }
}
