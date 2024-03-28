using TrackMagic.Application.Features.ColorIdentities.Get;

namespace TrackMagic.Application.UnitTests.Validators.Tests.ColorIdentity
{
    public class GetColorIdentityQueryValidatorTests : ValidatorTestBase
    {
        private readonly GetColorIdentityQueryValidator _validator;

        public GetColorIdentityQueryValidatorTests() : base("ColorIdentity")
            => _validator = new GetColorIdentityQueryValidator();

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        public async Task IdValidation(int id, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<GetColorIdentityQuery>()
                .With(ci => ci.Id, id);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.Errors.Count == 0);
        }
    }
}
