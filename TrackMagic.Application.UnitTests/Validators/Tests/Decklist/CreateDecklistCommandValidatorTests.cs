using TrackMagic.Application.Features.Decklists.Create;
using TrackMagic.Application.UnitTests.Validators.FakeServices;

namespace TrackMagic.Application.UnitTests.Validators.Tests.Decklist
{
    public class CreateDecklistCommandValidatorTests : ValidatorTestBase
    {
        private readonly CreateDecklistCommandValidator _validator;

        public CreateDecklistCommandValidatorTests() : base("Decklist")
            => _validator = new CreateDecklistCommandValidator(new FakeCardsService());

        [Theory]
        [InlineData(0, true)]
        [InlineData(1, false)]
        [InlineData(2, false)]
        [InlineData(3, false)]
        [InlineData(4, false)]
        [InlineData(5, false)]
        public async Task CardIdsValidation(int cardListsIndex, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<CreateDecklistCommand>()
                .With(dl => dl.CardIds, cardIdLists[cardListsIndex]);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.IsValid);
        }

        public List<int>[] cardIdLists =
        [
            Enumerable.Range(1, 100).ToList(),
            new List<int> { },
            Enumerable.Range(1, 10).ToList(),
            Enumerable.Range(1, 101).ToList(),
            Enumerable.Range(0, 100).ToList(),
            Enumerable.Range(2, 100).ToList()
        ];
    }
}
