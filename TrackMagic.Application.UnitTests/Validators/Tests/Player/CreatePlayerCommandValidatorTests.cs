using TrackMagic.Application.Features.Players.Create;
using TrackMagic.Application.UnitTests.Validators.FakeServices;

namespace TrackMagic.Application.UnitTests.Validators.Tests.Player
{
    public class CreatePlayerCommandValidatorTests : ValidatorTestBase
    {
        private readonly CreatePlayerCommandValidator _validator;

        public CreatePlayerCommandValidatorTests() : base("Player")
            => _validator = new CreatePlayerCommandValidator(new FakePlayersService());

        [Theory]
        [InlineData("Valid", true)]
        [InlineData("InvalidBecauseThisIsMoreThanTwentyCharacters", false)]
        public async Task FirstNameValidation(string name, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<CreatePlayerCommand>();
            command.FirstName = name;

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.Errors.Count == 0);
        }

        [Theory]
        [InlineData("Valid", true)]
        [InlineData("InvalidBecauseThisIsMoreThanFiftyCharactersThusExceedingTheLimit", false)]
        public async Task LastNameValidation(string name, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<CreatePlayerCommand>();
            command.LastName = name;

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.Errors.Count == 0);
        }

        [Theory]
        [InlineData("Bruce", "Banner", true)]
        [InlineData("Tony", "Hawks", true)]
        [InlineData("Stephen", "Strange", false)]
        public async Task DuplicateValidation(string firstName, string lastName, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<CreatePlayerCommand>();
            command.FirstName = firstName;
            command.LastName = lastName;

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.Errors.Count == 0);
        }
    }
}
