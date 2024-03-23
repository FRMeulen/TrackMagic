using TrackMagic.Application.Features.Players.Delete;
using TrackMagic.Application.UnitTests.Validators.FakeServices;

namespace TrackMagic.Application.UnitTests.Validators.Tests
{
    public class DeletePlayerCommandValidatorTests : ValidatorTestBase
    {
        private readonly DeletePlayerCommandValidator _validator;

        public DeletePlayerCommandValidatorTests() : base("Player")
            => _validator = new DeletePlayerCommandValidator(new FakePlayersService());

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(9001, false)]
        public async Task IdValidation(int id, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<DeletePlayerCommand>();
            command.Id = id;

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.Errors.Count == 0);
        }
    }
}
