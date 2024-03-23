using TrackMagic.Application.Dtos;
using TrackMagic.Application.Dtos.Base;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.UnitTests.Dtos.Tests
{
    public class PlayerDtoMappingTests : DtoMappingTestBase
    {
        [Fact]
        public void PlayerToPlayerDto()
        {
            // Arrange.
            var player = FixtureFactory.Create<Player>();

            // Act.
            var dto = Mapper.Map<PlayerDto>(player);

            // Assert.
            Assert.Equal(player.Id, dto.Id);
            Assert.Equal(player.FirstName, dto.FirstName);
            Assert.Equal(player.LastName, dto.LastName);
            Assert.Equal(player.Contested.Count, dto.Contested.Count);
            Assert.Equal(player.Decks.Count, dto.Decks.Count);
        }

        [Fact]
        public void PlayerToShallowDto()
        {
            // Arrange.
            var player = FixtureFactory.Create<Player>();

            // Act.
            var dto = Mapper.Map<ShallowDto<PlayerDto>>(player);

            // Assert.
            Assert.Equal(player.Id, dto.Id);
            Assert.Equal(player.FullName, dto.Name);
        }
    }
}
