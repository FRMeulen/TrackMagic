using TrackMagic.Application.Features.Decklists.Update;
using TrackMagic.Application.UnitTests.Validators.FakeServices;

namespace TrackMagic.Application.UnitTests.Validators.Tests.Decklist
{
    public class UpdateDecklistCommandValidatorTests : ValidatorTestBase
    {
        private readonly UpdateDecklistCommandValidator _validator;

        public UpdateDecklistCommandValidatorTests() : base("Decklist")
            => _validator = new UpdateDecklistCommandValidator(new FakeDecklistsService(), new FakeCardsService());

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(63, false)]
        public async Task IdValidation(int id, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<UpdateDecklistCommand>()
                .With(dl => dl.Id, id);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.IsValid);
        }

        [Theory]
        [MemberData(nameof(CardIdLists))]
        public async Task CardIdsValidation(List<int> cardIds, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<UpdateDecklistCommand>()
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
