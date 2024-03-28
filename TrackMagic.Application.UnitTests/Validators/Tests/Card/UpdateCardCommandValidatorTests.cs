using TrackMagic.Application.Features.Cards.Update;
using TrackMagic.Application.UnitTests.Validators.FakeServices;
using TrackMagic.Shared.Enums;

namespace TrackMagic.Application.UnitTests.Validators.Tests.Card
{
    public class UpdateCardCommandValidatorTests : ValidatorTestBase
    {
        private readonly UpdateCardCommandValidator _validator;

        public UpdateCardCommandValidatorTests() : base("Card")
            => _validator = new UpdateCardCommandValidator(new FakeCardsService());

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(900, false)]
        public async Task IdValidation(int id, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<UpdateCardCommand>()
                .With(c => c.Id, id);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.Errors.Count == 0);
        }

        [Theory]
        [InlineData("Valid", true)]
        [InlineData("InvalidBecauseTheUpperLimitIsExceededBecauseTheLengthOfThisStringIsTooLong", false)]
        public async Task NameValidation(string name, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<UpdateCardCommand>()
                .With(c => c.Name, name);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.Errors.Count == 0);
        }

        [Theory]
        [InlineData(0, true)]
        [InlineData(1, true)]
        [InlineData(2, false)]
        public async Task CardTypesValidation(int cardTypesIndex, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<UpdateCardCommand>()
                .With(c => c.CardTypes, cardTypeLists[cardTypesIndex]);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.Errors.Count == 0);
        }

        [Theory]
        [InlineData(1, "Temporary Lockdown", true)]
        [InlineData(1, "Invasion of Segovia", false)]
        public async Task DuplicateValidation(int id, string name, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<UpdateCardCommand>()
                .With(c => c.Id, id)
                .With(c => c.Name, name);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.Errors.Count == 0);
        }

        private static List<CardTypes>[] cardTypeLists =
        [
            new List<CardTypes> { CardTypes.Sorcery },
            new List<CardTypes> { CardTypes.Enchantment, CardTypes.Creature },
            new List<CardTypes> { }
        ];
    }
}
