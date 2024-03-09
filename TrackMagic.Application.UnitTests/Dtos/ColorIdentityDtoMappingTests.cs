using TrackMagic.Application.Dtos;
using TrackMagic.Application.Dtos.Base;
using TrackMagic.Domain.Entities;
using TrackMagic.Testing.Shared.Fixtures;

namespace TrackMagic.Application.UnitTests.Dtos
{
    public class ColorIdentityDtoMappingTests : BaseDtoMappingTests
    {
        [Fact]
        public void ColorIdentityToColorIdentityDto()
        {
            // Arrange.
            var colorIdentity = new Fixture<ColorIdentity>().Create();

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
            var colorIdentity = new Fixture<ColorIdentity>().Create();

            // Act.
            var dto = Mapper.Map<ShallowDto<ColorIdentityDto>>(colorIdentity);

            // Assert.
            Assert.Equal(colorIdentity.Id, dto.Id);
            Assert.Equal(colorIdentity.Name, dto.Name);
        }
    }
}