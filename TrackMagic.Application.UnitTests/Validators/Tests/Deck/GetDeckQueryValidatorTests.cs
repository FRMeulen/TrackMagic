using TrackMagic.Application.Features.Decks.Get;

namespace TrackMagic.Application.UnitTests.Validators.Tests.Deck
{
    public class GetDeckQueryValidatorTests : ValidatorTestBase
    {
        private readonly GetDeckQueryValidator _validator;

        public GetDeckQueryValidatorTests() : base("Deck")
            => _validator = new GetDeckQueryValidator();

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        public async Task IdValidation(int id, bool success)
        {
            // Arrange.
            var query = FixtureFactory.Create<GetDeckQuery>()
                .With(d => d.Id, id);

            // Act.
            var result = await _validator.ValidateAsync(query);

            // Assert.
            Assert.Equal(success, result.IsValid);
        }
    }
}
