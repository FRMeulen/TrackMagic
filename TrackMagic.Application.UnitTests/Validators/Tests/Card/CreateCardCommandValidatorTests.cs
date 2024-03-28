using TrackMagic.Application.Features.Cards.Create;
using TrackMagic.Application.UnitTests.Validators.FakeServices;
using TrackMagic.Shared.Enums;

namespace TrackMagic.Application.UnitTests.Validators.Tests.Card
{
    public class CreateCardCommandValidatorTests : ValidatorTestBase
    {
        private readonly CreateCardCommandValidator _validator;

        public CreateCardCommandValidatorTests() : base("Card")
            => _validator = new CreateCardCommandValidator(new FakeCardsService());

        [Theory]
        [InlineData("Liliana, Dreadhorde General", true)]
        [InlineData("Portal to Phyrexia", false)]
        [InlineData("", false)]
        [InlineData("WayTooLongCardNameThatExceedsFiftyCharactersWhichIsTheLimit", false)]
        public async Task NameValidation(string name, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<CreateCardCommand>()
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
            var command = FixtureFactory.Create<CreateCardCommand>()
                .With(c => c.CardTypes, cardTypeLists[cardTypesIndex]);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.Errors.Count == 0);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        public async Task ColorIdentityIdValidation(int id, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<CreateCardCommand>()
                .With(c => c.ColorIdentityId, id);

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
