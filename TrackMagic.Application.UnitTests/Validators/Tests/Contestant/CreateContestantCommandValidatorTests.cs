using TrackMagic.Application.Features.Contestants.Create;
using TrackMagic.Application.UnitTests.Validators.FakeServices;

namespace TrackMagic.Application.UnitTests.Validators.Tests.Contestant
{
    public class CreateContestantCommandValidatorTests : ValidatorTestBase
    {
        private readonly CreateContestantCommandValidator _validator;

        public CreateContestantCommandValidatorTests() : base("Contestant")
            => _validator = new CreateContestantCommandValidator(new FakeGamesService(), new FakePlayersService(), new FakeDecksService());

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(751, false)]
        public async Task GameIdValidation(int id, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<CreateContestantCommand>()
                .With(c => c.GameId, id);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.Errors.Count == 0);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(21, false)]
        public async Task PlayerIdValidation(int id, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<CreateContestantCommand>()
                .With(c => c.PlayerId, id);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.Errors.Count == 0);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(5234, false)]
        public async Task DeckIdValidation(int id, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<CreateContestantCommand>()
                .With(c => c.DeckId, id);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.Errors.Count == 0);
        }
    }
}
