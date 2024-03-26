using TrackMagic.Application.Features.ColorIdentities.Delete;
using TrackMagic.Application.UnitTests.Validators.FakeServices;

namespace TrackMagic.Application.UnitTests.Validators.Tests.ColorIdentity
{
    public class DeleteColorIdentityCommandValidatorTests : ValidatorTestBase
    {
        private readonly DeleteColorIdentityCommandValidator _validator;

        public DeleteColorIdentityCommandValidatorTests() : base("ColorIdentity")
            => _validator = new DeleteColorIdentityCommandValidator(new FakeColorIdentitiesService());

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        [InlineData(70, false)]
        public async Task IdValidation(int id, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<DeleteColorIdentityCommand>();
            command.Id = id;

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.Errors.Count == 0);
        }
    }
}
