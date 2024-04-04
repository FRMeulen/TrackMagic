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
        [MemberData(nameof(CardIdLists))]
        public async Task CardIdsValidation(List<int> cardIds, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<CreateDecklistCommand>()
                .With(dl => dl.CardIds, cardIds);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.IsValid);
        }

        public static IEnumerable<object[]> CardIdLists()
        {
            yield return new object[] { Enumerable.Range(1, 100).ToList(), true };
            yield return new object[] { new List<int> { }, false };
            yield return new object[] { Enumerable.Range(1, 10).ToList(), false };
            yield return new object[] { Enumerable.Range(1, 101).ToList(), false };
            yield return new object[] { Enumerable.Range(0, 99).ToList(), false };
            yield return new object[] { Enumerable.Range(2, 101).ToList(), false };
        }
    }
}
