using TrackMagic.Application.Features.Cards.GetByName;
using TrackMagic.Application.UnitTests.Validators.FakeServices;

namespace TrackMagic.Application.UnitTests.Validators.Tests.Card
{
    public class GetCardByNameQueryValidatorTests : ValidatorTestBase
    {
        private readonly GetCardByNameQueryValidator _validator;

        public GetCardByNameQueryValidatorTests() : base("Card")
            => _validator = new GetCardByNameQueryValidator(new FakeCardsService());

        [Theory]
        [InlineData("Portal to Phyrexia", true)]
        [InlineData("Memory Deluge", false)]
        public async Task NameValidation(string name, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<GetCardByNameQuery>()
                .With(c => c.Name, name);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.IsValid);
        }
    }
}
