using TrackMagic.Application.Features.Decklists.Get;

namespace TrackMagic.Application.UnitTests.Validators.Tests.Decklist
{
    public class GetDecklistQueryValidatorTests : ValidatorTestBase
    {
        private readonly GetDecklistQueryValidator _validator;

        public GetDecklistQueryValidatorTests() : base("Decklist")
            => _validator = new GetDecklistQueryValidator();

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        public async Task IdValidation(int id, bool success)
        {
            // Arrange.
            var query = FixtureFactory.Create<GetDecklistQuery>()
                .With(dl => dl.Id, id);

            // Act.
            var result = await _validator.ValidateAsync(query);

            // Assert.
            Assert.Equal(success, result.IsValid);
        }
    }
}
