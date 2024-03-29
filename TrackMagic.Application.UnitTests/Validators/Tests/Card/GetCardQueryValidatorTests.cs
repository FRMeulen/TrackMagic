using TrackMagic.Application.Features.Cards.Get;

namespace TrackMagic.Application.UnitTests.Validators.Tests.Card
{
    public class GetCardQueryValidatorTests : ValidatorTestBase
    {
        private readonly GetCardQueryValidator _validator;

        public GetCardQueryValidatorTests() : base("Card")
            => _validator = new GetCardQueryValidator();

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        public async Task IdValidation(int id, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<GetCardQuery>()
                .With(c => c.Id, id);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.IsValid);
        }
    }
}
