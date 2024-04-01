using TrackMagic.Application.Features.Decklists.Delete;
using TrackMagic.Application.UnitTests.Validators.FakeServices;

namespace TrackMagic.Application.UnitTests.Validators.Tests.Decklist
{
    public class DeleteDecklistCommandValidatorTests : ValidatorTestBase
    {
        private readonly DeleteDecklistCommandValidator _validator;

        public DeleteDecklistCommandValidatorTests() : base("Decklist")
            => _validator = new DeleteDecklistCommandValidator(new FakeDecklistsService());

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(45, false)]
        public async Task IdValidation(int id, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<DeleteDecklistCommand>()
                .With(dl => dl.Id, id);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.IsValid);
        }
    }
}
