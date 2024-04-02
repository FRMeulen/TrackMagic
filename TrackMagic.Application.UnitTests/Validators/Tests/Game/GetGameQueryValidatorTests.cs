using TrackMagic.Application.Features.Games.Get;
using TrackMagic.Application.UnitTests.Validators.FakeServices;

namespace TrackMagic.Application.UnitTests.Validators.Tests.Game
{
    public class GetGameQueryValidatorTests : ValidatorTestBase
    {
        private readonly GetGameQueryValidator _validator;

        public GetGameQueryValidatorTests() : base("Game")
            => _validator = new GetGameQueryValidator(new FakeGamesService());

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(15, false)]
        public async Task IdValidation(int id, bool success)
        {
            // Arrange.
            var query = FixtureFactory.Create<GetGameQuery>()
                .With(g => g.Id, id);

            // Act.
            var result = await _validator.ValidateAsync(query);

            // Assert.
            Assert.Equal(success, result.IsValid);
        }
    }
}
