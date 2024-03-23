using TrackMagic.Application.Dtos;
using TrackMagic.Application.Dtos.Base;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.UnitTests.Dtos.Tests
{
    public class DeckDtoMappingTests : DtoMappingTestBase
    {
        [Fact]
        public void DeckToDeckDto()
        {
            // Arrange.
            var deck = FixtureFactory.Create<Deck>();

            // Act.
            var dto = Mapper.Map<DeckDto>(deck);

            // Assert.
            Assert.Equal(deck.Id, dto.Id);
            Assert.Equal(deck.Name, dto.Name);
            Assert.Equal(deck.Owner.Id, dto.Owner.Id);
            Assert.Equal(deck.Commanders.Count, dto.Commanders.Count);
            Assert.Equal(deck.Decklist.Id, dto.Decklist.Id);
            Assert.Equal(deck.PilotedBy.Count, dto.PilotedBy.Count);
        }

        [Fact]
        public void DeckToShallowDto()
        {
            // Arrange.
            var deck = FixtureFactory.Create<Deck>();

            // Act.
            var dto = Mapper.Map<ShallowDto<DeckDto>>(deck);

            // Assert.
            Assert.Equal(deck.Id, dto.Id);
            Assert.Equal(deck.Name, dto.Name);
        }
    }
}
