using TrackMagic.Application.Features.ColorIdentities.Update;
using TrackMagic.Application.UnitTests.Validators.FakeServices;

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
            var command = FixtureFactory.Create<UpdateColorIdentityCommand>();
            command.Id = id;

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.Errors.Count == 0);
        }

        [Theory]
        [InlineData("Valid", true)]
        [InlineData("InvalidBecauseThisIsMoreThanFiftyCharactersThusExceedingTheMaximumLimit", false)]
        public async Task NameValidation(string name, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<UpdateColorIdentityCommand>();
            command.Name = name;

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.Errors.Count == 0);
        }

        [Theory]
        [InlineData(1, "Valid", true)]
        [InlineData(1, "Mardu", false)]
        public async Task DuplicateValidation(int id, string name, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<UpdateColorIdentityCommand>();
            command.Id = id;
            command.Name = name;

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.Errors.Count == 0);
        }
    }
}
