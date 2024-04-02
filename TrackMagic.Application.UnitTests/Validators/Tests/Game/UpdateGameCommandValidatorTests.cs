using TrackMagic.Application.Features.Games.Update;
using TrackMagic.Application.UnitTests.Validators.FakeServices;

namespace TrackMagic.Application.UnitTests.Validators.Tests.Game
{
    public class UpdateGameCommandValidatorTests : ValidatorTestBase
    {
        private readonly UpdateGameCommandValidator _validator;

        public UpdateGameCommandValidatorTests() : base("Game")
            => _validator = new UpdateGameCommandValidator(new FakeGamesService());

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(84, false)]
        public async Task IdValidation(int id, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<UpdateGameCommand>()
                .With(g => g.Id, id);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.IsValid);
        }

        [Theory]
        [InlineData(0, true)]
        [InlineData(1, true)]
        [InlineData(2, false)]
        public async Task DateValidation(int datetimesIndex, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<UpdateGameCommand>()
                .With(g => g.Date, dateTimes[datetimesIndex]);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.IsValid);
        }

        [Theory]
        [InlineData(9, true)]
        [InlineData(0, false)]
        public async Task LengthInCyclesValidation(int lengthInCycles, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<UpdateGameCommand>()
                .With(g => g.LengthInCycles, lengthInCycles);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.IsValid);
        }

        public DateTimeOffset[] dateTimes =
        [
            DateTimeOffset.UtcNow,
            new DateTimeOffset(2020, 4, 1, 12, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(3000, 1, 1, 0, 0, 0, TimeSpan.Zero)
        ];
    }
}
