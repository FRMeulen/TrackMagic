namespace TrackMagic.Application.UnitTests.Validators
{
    public abstract class ValidatorTestBase
    {
        protected readonly FixtureFactory FixtureFactory;

        public ValidatorTestBase(string typeName)
            => FixtureFactory = new FixtureFactory($"Validators/Fixtures/{typeName}/");
    }
}
