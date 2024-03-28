using TrackMagic.Application.Features.Players.Get;

namespace TrackMagic.Application.UnitTests.Validators.Tests.Player
{
    public class GetPlayerQueryValidatorTests : ValidatorTestBase
    {
        private readonly GetPlayerQueryValidator _validator;

        public GetPlayerQueryValidatorTests() : base("Player")
            => _validator = new GetPlayerQueryValidator();

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        public async Task IdValidation(int id, bool success)
        {
            // Arrange.
            var query = FixtureFactory.Create<GetPlayerQuery>()
                .With(p => p.Id, id);

            // Act.
            var result = await _validator.ValidateAsync(query);

            // Assert.
            Assert.Equal(success, result.Errors.Count == 0);
        }
    }
}
