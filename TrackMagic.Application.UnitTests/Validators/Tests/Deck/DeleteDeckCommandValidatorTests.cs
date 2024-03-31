using TrackMagic.Application.Features.Decks.Delete;
using TrackMagic.Application.UnitTests.Validators.FakeServices;

namespace TrackMagic.Application.UnitTests.Validators.Tests.Deck
{
    public class DeleteDeckCommandValidatorTests : ValidatorTestBase
    {
        private readonly DeleteDeckCommandValidator _validator;

        public DeleteDeckCommandValidatorTests() : base("Deck")
            => _validator = new DeleteDeckCommandValidator(new FakeDecksService());

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(12, false)]
        public async Task IdValidation(int id, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<DeleteDeckCommand>()
                .With(d => d.Id, id);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.IsValid);
        }
    }
}
