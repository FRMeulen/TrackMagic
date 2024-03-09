using TrackMagic.Application.Dtos;
using TrackMagic.Application.Dtos.Base;
using TrackMagic.Domain.Entities;
using TrackMagic.Testing.Shared.Fixtures;

namespace TrackMagic.Application.UnitTests.Dtos
{
    public class ContestantDtoMappingTests : BaseDtoMappingTests
    {
        [Fact]
        public void ContestantToContestantDto()
        {
            // Arrange.
            var contestant = new Fixture<Contestant>().Create();

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
            var contestant = new Fixture<Contestant>().Create();

            // Act.
            var dto = Mapper.Map<ShallowDto<ContestantDto>>(contestant);

            // Assert.
            Assert.Equal(contestant.Id, dto.Id);
            Assert.Equal(contestant.Id.ToString(), dto.Name);
        }
    }
}
