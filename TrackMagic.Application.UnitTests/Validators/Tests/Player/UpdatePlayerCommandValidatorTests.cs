﻿using TrackMagic.Application.Features.Players.Update;
using TrackMagic.Application.UnitTests.Validators.FakeServices;

namespace TrackMagic.Application.UnitTests.Validators.Tests.Player
{
    public class UpdatePlayerCommandValidatorTests : ValidatorTestBase
    {
        private readonly UpdatePlayerCommandValidator _validator;

        public UpdatePlayerCommandValidatorTests() : base("Player")
            => _validator = new UpdatePlayerCommandValidator(new FakePlayersService());

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(9001, false)]
        public async Task IdValidation(int id, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<UpdatePlayerCommand>();
            command.Id = id;

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.Errors.Count == 0);
        }

        [Theory]
        [InlineData("Valid", true)]
        [InlineData("InvalidBecauseThisIsMoreThanTwentyCharacters", false)]
        [InlineData("", false)]
        public async Task FirstNameValidation(string name, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<UpdatePlayerCommand>();
            command.FirstName = name;

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.Errors.Count == 0);
        }

        [Theory]
        [InlineData("Valid", true)]
        [InlineData("InvalidBecauseThisIsMoreThanFiftyCharactersExceedingTheMaximumLimit", false)]
        [InlineData("", false)]
        public async Task LastNameValidation(string name, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<UpdatePlayerCommand>();
            command.LastName = name;

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.Errors.Count == 0);
        }

        [Theory]
        [InlineData(1, "Rocket", "Raccoon", true)]
        [InlineData(1, "Stephen", "Notsostrange", true)]
        [InlineData(1, "Captain", "Rodgers", true)]
        [InlineData(1, "Steve", "Rodgers", false)]
        public async Task DuplicateValidation(int id, string firstName, string lastName, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<UpdatePlayerCommand>();
            command.Id = id;
            command.FirstName = firstName;
            command.LastName = lastName;

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.Errors.Count == 0);
        }
    }
}