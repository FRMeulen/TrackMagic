using TrackMagic.Application.Dtos;
using TrackMagic.Application.Dtos.Base;
using TrackMagic.Domain.Entities;
using TrackMagic.Testing.Shared.Fixtures;

namespace TrackMagic.Application.UnitTests.Dtos
{
    public class GameDtoMappingTests : BaseDtoMappingTests
    {
        [Fact]
        public void GameToGameDto()
        {
            // Arrange.
            var game = new Fixture<Game>().Create();

            // Act.
            var dto = Mapper.Map<GameDto>(game);

            // Assert.
            Assert.Equal(game.Id, dto.Id);
            Assert.Equal(game.Date, dto.Date);
            Assert.Equal(game.LengthInCycles, dto.LengthInCycles);
            Assert.Equal(game.Contestants.Count, dto.Contestants.Count);
        }

        [Fact]
        public void GameToShallowDto()
        {
            // Arrange.
            var game = new Fixture<Game>().Create();

            // Act.
            var dto = Mapper.Map<ShallowDto<GameDto>>(game);

            // Assert.
            Assert.Equal(game.Id, dto.Id);
            Assert.Equal(game.Id.ToString(), dto.Name);
        }
    }
}
