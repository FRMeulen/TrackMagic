using System.Collections;
using TrackMagic.Application.Features.ColorIdentities.Create;
using TrackMagic.Application.UnitTests.Validators.FakeServices;
using TrackMagic.Shared.Enums;

namespace TrackMagic.Application.UnitTests.Validators.Tests.ColorIdentity
{
    public class CreateColorIdentityCommandValidatorTests : ValidatorTestBase
    {
        private readonly CreateColorIdentityCommandValidator _validator;

        public CreateColorIdentityCommandValidatorTests() : base("ColorIdentity")
            => _validator = new CreateColorIdentityCommandValidator(new FakeColorIdentitiesService());

        [Theory]
        [InlineData("Dimir", true)]
        [InlineData("Mardu", false)]
        public async Task NameValidation(string name, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<CreateColorIdentityCommand>()
                .With(ci => ci.Name, name);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.IsValid);
        }

        [Theory]
        [MemberData(nameof(ColorLists))]
        public async Task ColorsValidation(List<Colors> colors, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<CreateColorIdentityCommand>()
                .With(ci => ci.Colors, colors);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.IsValid);
        }

        public static IEnumerable<object[]> ColorLists()
        {
            yield return new object[] { new List<Colors> { Colors.Red, Colors.Green }, true };
            yield return new object[] { new List<Colors> { }, true };
            yield return new object[] { new List<Colors> { Colors.Blue, Colors.Blue }, false };
            yield return new object[] { new List<Colors> { Colors.White, Colors.Blue, Colors.Black, Colors.Red, Colors.Green, Colors.White }, false };
        }
    }
}
