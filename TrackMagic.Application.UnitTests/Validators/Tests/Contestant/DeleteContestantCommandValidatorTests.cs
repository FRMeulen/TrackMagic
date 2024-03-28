using TrackMagic.Application.Features.Contestants.Delete;
using TrackMagic.Application.UnitTests.Validators.FakeServices;

namespace TrackMagic.Application.UnitTests.Validators.Tests.Contestant
{
    public class DeleteContestantCommandValidatorTests : ValidatorTestBase
    {
        private readonly DeleteContestantCommandValidator _validator;

        public DeleteContestantCommandValidatorTests() : base("Contestant")
            => _validator = new DeleteContestantCommandValidator(new FakeContestantsService());

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(6347, false)]
        public async Task IdValidation(int id, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<DeleteContestantCommand>()
                .With(c => c.Id, id);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.Errors.Count == 0);
        }
    }
}
