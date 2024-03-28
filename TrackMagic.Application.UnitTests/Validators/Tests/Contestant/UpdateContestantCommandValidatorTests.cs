using TrackMagic.Application.Features.Contestants.Update;
using TrackMagic.Application.UnitTests.Validators.FakeServices;

namespace TrackMagic.Application.UnitTests.Validators.Tests.Contestant
{
    public class UpdateContestantCommandValidatorTests : ValidatorTestBase
    {
        private readonly UpdateContestantCommandValidator _validator;

        public UpdateContestantCommandValidatorTests() : base("Contestant")
            => _validator = new UpdateContestantCommandValidator(
                new FakeContestantsService(),
                new FakeGamesService(),
                new FakePlayersService(),
                new FakeDecksService());

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(10, false)]
        public async Task IdValidation(int id, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<UpdateContestantCommand>()
                .With(c => c.Id, id);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.Errors.Count == 0);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(999, false)]
        public async Task GameIdValidation(int id, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<UpdateContestantCommand>()
                .With(c => c.GameId, id);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.Errors.Count == 0);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(705, false)]
        public async Task PlayerIdValidation(int id, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<UpdateContestantCommand>()
                .With(c => c.PlayerId, id);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.Errors.Count == 0);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(27, false)]
        public async Task DeckIdValidation(int id, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<UpdateContestantCommand>()
                .With(c => c.DeckId, id);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.Errors.Count == 0);
        }
    }
}
