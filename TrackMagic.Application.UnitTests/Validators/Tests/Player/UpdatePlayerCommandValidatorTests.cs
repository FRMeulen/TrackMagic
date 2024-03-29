using TrackMagic.Application.Features.Players.Update;
using TrackMagic.Application.UnitTests.Validators.FakeServices;

namespace TrackMagic.Application.UnitTests.Validators.Tests.Player
{
    public class UpdatePlayerCommandValidatorTests : ValidatorTestBase
    {
        private readonly UpdatePlayerCommandValidator _validator;

        public UpdatePlayerCommandValidatorTests() : base("Player")
            => _validator = new UpdatePlayerCommandValidator(new FakePlayersService());

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(9001, false)]
        public async Task IdValidation(int id, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<UpdatePlayerCommand>()
                .With(p => p.Id, id);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.IsValid);
        }

        [Theory]
        [InlineData("Valid", true)]
        [InlineData("InvalidBecauseThisIsMoreThanTwentyCharacters", false)]
        [InlineData("", false)]
        public async Task FirstNameValidation(string name, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<UpdatePlayerCommand>()
                .With(p => p.FirstName, name);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.IsValid);
        }

        [Theory]
        [InlineData("Valid", true)]
        [InlineData("InvalidBecauseThisIsMoreThanFiftyCharactersExceedingTheMaximumLimit", false)]
        [InlineData("", false)]
        public async Task LastNameValidation(string name, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<UpdatePlayerCommand>()
                .With(p => p.LastName, name);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.IsValid);
        }

        [Theory]
        [InlineData(1, "Rocket", "Raccoon", true)]
        [InlineData(1, "Stephen", "Notsostrange", true)]
        [InlineData(1, "Captain", "Rodgers", true)]
        [InlineData(1, "Steve", "Rodgers", false)]
        public async Task DuplicateValidation(int id, string firstName, string lastName, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<UpdatePlayerCommand>()
                .With(p => p.Id, id)
                .With(p => p.FirstName, firstName)
                .With(p => p.LastName, lastName);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.IsValid);
        }
    }
}
