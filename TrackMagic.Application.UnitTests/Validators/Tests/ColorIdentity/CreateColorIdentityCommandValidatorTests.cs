using TrackMagic.Application.Features.ColorIdentities.Create;
using TrackMagic.Application.UnitTests.Validators.FakeServices;

namespace TrackMagic.Application.UnitTests.Validators.Tests.ColorIdentity
{
    public class CreateColorIdentityCommandValidatorTests : ValidatorTestBase
    {
        private readonly CreateColorIdentityCommandValidator _validator;

        public CreateColorIdentityCommandValidatorTests() : base("ColorIdentity")
            => _validator = new CreateColorIdentityCommandValidator(new FakeColorIdentitiesService());

        [Theory]
        [InlineData("Dimir", true)]
        [InlineData("Mardu", false)]
        public async Task NameValidation(string name, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<CreateColorIdentityCommand>();
            command.Name = name;

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.Errors.Count == 0);
        }
    }
}
