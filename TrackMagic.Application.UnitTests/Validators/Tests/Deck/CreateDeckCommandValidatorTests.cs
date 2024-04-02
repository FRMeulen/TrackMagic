using TrackMagic.Application.Features.Decks.Create;
using TrackMagic.Application.UnitTests.Validators.FakeServices;

namespace TrackMagic.Application.UnitTests.Validators.Tests.Deck
{
    public class CreateDeckCommandValidatorTests : ValidatorTestBase
    {
        private readonly CreateDeckCommandValidator _validator;

        public CreateDeckCommandValidatorTests() : base("Deck")
            => _validator = new CreateDeckCommandValidator(
                new FakeDecksService(),
                new FakeCardsService(),
                new FakePlayersService(),
                new FakeDecklistsService());

        [Theory]
        [InlineData("Enter the Fungeon", true)]
        [InlineData("Saprecursion", false)]
        [InlineData("", false)]
        public async Task NameValidation(string name, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<CreateDeckCommand>()
                .With(d => d.Name, name);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.IsValid);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(64, false)]
        public async Task OwnerIdValidation(int id, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<CreateDeckCommand>()
                .With(d => d.OwnerId, id);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.IsValid);
        }

        [Theory]
        [InlineData(0, true)]
        [InlineData(1, false)]
        public async Task CommanderIdsValidation(int listIndex, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<CreateDeckCommand>()
                .With(d => d.CommanderIds, CommanderLists[listIndex]);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.IsValid);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(180, false)]
        [InlineData(null, true)]
        public async Task CompanionIdValidation(int? id, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<CreateDeckCommand>()
                .With(d => d.CompanionId, id);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.IsValid);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(5, false)]
        public async Task DecklistIdValidation(int id, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<CreateDeckCommand>()
                .With(d => d.DecklistId, id);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.IsValid);
        }

        public List<int>[] CommanderLists =
        [
            new List<int> { 1, 2 },
            new List<int> { }
        ];
    }
}
