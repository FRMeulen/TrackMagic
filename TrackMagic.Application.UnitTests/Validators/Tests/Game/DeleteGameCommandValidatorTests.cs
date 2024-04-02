using TrackMagic.Application.Features.Games.Delete;
using TrackMagic.Application.UnitTests.Validators.FakeServices;

namespace TrackMagic.Application.UnitTests.Validators.Tests.Game
{
    public class DeleteGameCommandValidatorTests : ValidatorTestBase
    {
        private readonly DeleteGameCommandValidator _validator;

        public DeleteGameCommandValidatorTests() : base("Game")
            => _validator = new DeleteGameCommandValidator(new FakeGamesService());

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(6, false)]
        public async Task IdValidation(int id, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<DeleteGameCommand>()
                .With(g => g.Id, id);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.IsValid);
        }
    }
}
