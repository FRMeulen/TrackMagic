﻿using TrackMagic.Application.Features.Decks.Create;
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
        [MemberData(nameof(CommanderLists))]
        public async Task CommanderIdsValidation(List<int> commanderIds, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<CreateDeckCommand>()
                .With(d => d.CommanderIds, commanderIds);

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

        public static IEnumerable<object[]> CommanderLists()
        {
            yield return new object[] { new List<int> { 1, 2 }, true };
            yield return new object[] { new List<int> { }, false };
        }
    }
}
