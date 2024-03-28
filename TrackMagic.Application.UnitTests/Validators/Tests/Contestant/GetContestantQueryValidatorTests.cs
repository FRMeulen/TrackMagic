using TrackMagic.Application.Features.Contestants.Get;
using TrackMagic.Application.UnitTests.Validators.FakeServices;

namespace TrackMagic.Application.UnitTests.Validators.Tests.Contestant
{
    public class GetContestantQueryValidatorTests : ValidatorTestBase
    {
        private readonly GetContestantQueryValidator _validator;

        public GetContestantQueryValidatorTests() : base("Contestant")
            => _validator = new GetContestantQueryValidator(new FakeContestantsService());

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        public async Task IdValidation(int id, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<GetContestantQuery>()
                .With(c => c.Id, id);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.Errors.Count == 0);
        }
    }
}
