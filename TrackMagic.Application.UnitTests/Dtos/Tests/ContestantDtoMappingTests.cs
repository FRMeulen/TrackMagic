using TrackMagic.Application.Dtos;
using TrackMagic.Application.Dtos.Base;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.UnitTests.Dtos.Tests
{
    public class ContestantDtoMappingTests : BaseDtoMappingTests
    {
        [Fact]
        public void ContestantToContestantDto()
        {
            // Arrange.
            var contestant = FixtureFactory.Create<Contestant>();

            // Act.
            var dto = Mapper.Map<ContestantDto>(contestant);

            // Assert.
            Assert.Equal(contestant.Id, dto.Id);
            Assert.Equal(contestant.Points, dto.Points);
            Assert.Equal(contestant.Game.Id, dto.Game.Id);
            Assert.Equal(contestant.Player.Id, dto.Player.Id);
            Assert.Equal(contestant.Deck.Id, dto.Deck.Id);
        }

        [Fact]
        public void ContestantToShallowDto()
        {
            // Arrange.
            var contestant = FixtureFactory.Create<Contestant>();

            // Act.
            var dto = Mapper.Map<ShallowDto<ContestantDto>>(contestant);

            // Assert.
            Assert.Equal(contestant.Id, dto.Id);
            Assert.Equal(contestant.Id.ToString(), dto.Name);
        }
    }
}
