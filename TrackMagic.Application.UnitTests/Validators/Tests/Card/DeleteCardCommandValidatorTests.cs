using TrackMagic.Application.Features.Cards.Delete;
using TrackMagic.Application.UnitTests.Validators.FakeServices;

namespace TrackMagic.Application.UnitTests.Validators.Tests.Card
{
    public class DeleteCardCommandValidatorTests : ValidatorTestBase
    {
        private readonly DeleteCardCommandValidator _validator;

        public DeleteCardCommandValidatorTests() : base("Card")
            => _validator = new DeleteCardCommandValidator(new FakeCardsService());

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(851, false)]
        public async Task IdValidation(int id, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<DeleteCardCommand>()
                .With(c => c.Id, id);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.Errors.Count == 0);
        }
    }
}
