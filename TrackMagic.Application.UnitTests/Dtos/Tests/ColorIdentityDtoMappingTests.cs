using TrackMagic.Application.Dtos;
using TrackMagic.Application.Dtos.Base;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.UnitTests.Dtos.Tests
{
    public class ColorIdentityDtoMappingTests : DtoMappingTestBase
    {
        [Fact]
        public void ColorIdentityToColorIdentityDto()
        {
            // Arrange.
            var colorIdentity = FixtureFactory.Create<ColorIdentity>();

            // Act.
            var dto = Mapper.Map<ColorIdentityDto>(colorIdentity);

            // Assert.
            Assert.Equal(colorIdentity.Id, dto.Id);
            Assert.Equal(colorIdentity.Name, dto.Name);
            Assert.Equal(colorIdentity.Colors, dto.Colors);
            Assert.Equal(colorIdentity.CardsInIdentity.Count, dto.CardsInIdentity.Count);
        }

        [Fact]
        public void ColorIdentityToShallowDto()
        {
            // Arrange.
            var colorIdentity = FixtureFactory.Create<ColorIdentity>();

            // Act.
            var dto = Mapper.Map<ShallowDto<ColorIdentityDto>>(colorIdentity);

            // Assert.
            Assert.Equal(colorIdentity.Id, dto.Id);
            Assert.Equal(colorIdentity.Name, dto.Name);
        }
    }
}