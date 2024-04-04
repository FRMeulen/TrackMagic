using FluentValidation;
using TrackMagic.Application.Features.ColorIdentities.Create;
using TrackMagic.Application.Features.ColorIdentities.Update;
using TrackMagic.Application.UnitTests.Validators.FakeServices;
using TrackMagic.Shared.Enums;

namespace TrackMagic.Application.UnitTests.Validators.Tests.ColorIdentity
{
    public class UpdateColorIdentityCommandValidatorTests : ValidatorTestBase
    {
        private readonly UpdateColorIdentityCommandValidator _validator;

        public UpdateColorIdentityCommandValidatorTests() : base("ColorIdentity")
            => _validator = new UpdateColorIdentityCommandValidator(new FakeColorIdentitiesService());

        [Theory]
        [InlineData(1, true)]
        [InlineData(70, false)]
        public async Task IdValidation(int id, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<UpdateColorIdentityCommand>()
                .With(ci => ci.Id, id);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.IsValid);
        }

        [Theory]
        [InlineData("Valid", true)]
        [InlineData("InvalidBecauseThisIsMoreThanFiftyCharactersThusExceedingTheMaximumLimit", false)]
        public async Task NameValidation(string name, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<UpdateColorIdentityCommand>()
                .With(ci => ci.Name, name);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.IsValid);
        }

        [Theory]
        [InlineData(1, "Valid", true)]
        [InlineData(1, "Mardu", false)]
        public async Task DuplicateValidation(int id, string name, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<UpdateColorIdentityCommand>()
                .With(ci => ci.Id, id)
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
            var command = FixtureFactory.Create<UpdateColorIdentityCommand>()
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
