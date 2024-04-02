using TrackMagic.Application.Features.Games.CreateShallow;
using TrackMagic.Application.UnitTests.Validators.FakeServices;

namespace TrackMagic.Application.UnitTests.Validators.Tests.Game
{
    public class CreateShallowGameCommandValidatorTests : ValidatorTestBase
    {
        private readonly CreateShallowGameCommandValidator _validator;

        public CreateShallowGameCommandValidatorTests() : base("Game")
            => _validator = new CreateShallowGameCommandValidator(new FakePlayersService(), new FakeDecksService());

        [Theory]
        [MemberData(nameof(TestContestants))]
        public async Task ContestantsValidation(List<ContestantForShallowGame> contestants, bool success)
        {
            // Arrange.
            var command = FixtureFactory.Create<CreateShallowGameCommand>()
                .With(g => g.Contestants, contestants);

            // Act.
            var result = await _validator.ValidateAsync(command);

            // Assert.
            Assert.Equal(success, result.IsValid);
        }

        public static IEnumerable<object[]> TestContestants()
        {
            yield return new object[]
            {
                new List<ContestantForShallowGame>
                {
                    new ContestantForShallowGame { Points = 300, FirstName = "Tony", LastName = "Stark", DeckName = "Saprecursion"  },
                    new ContestantForShallowGame { Points = 200, FirstName = "Steve", LastName = "Rodgers", DeckName = "Cursed Gifts" },
                    new ContestantForShallowGame { Points = 100, FirstName = "Stephen", LastName = "Strange", DeckName = "Token Mania" },
                    new ContestantForShallowGame { Points = 0, FirstName = "Peter", LastName = "Parker", DeckName = "Free Real Estate" },
                }, true
            };

            yield return new object[]
            {
                new List<ContestantForShallowGame>
                {
                    new ContestantForShallowGame { Points = 300, FirstName = "Rocket", LastName = "Stark", DeckName = "Saprecursion"  },
                    new ContestantForShallowGame { Points = 200, FirstName = "Clint", LastName = "Rodgers", DeckName = "Cursed Gifts" },
                    new ContestantForShallowGame { Points = 100, FirstName = "Natasha", LastName = "Strange", DeckName = "Token Mania" },
                    new ContestantForShallowGame { Points = 0, FirstName = "Bruce", LastName = "Parker", DeckName = "Free Real Estate" },
                }, false
            };

            yield return new object[]
            {
                new List<ContestantForShallowGame>
                {
                    new ContestantForShallowGame { Points = 300, FirstName = "Tony", LastName = "Krats", DeckName = "Saprecursion"  },
                    new ContestantForShallowGame { Points = 200, FirstName = "Steve", LastName = "Sregdor", DeckName = "Cursed Gifts" },
                    new ContestantForShallowGame { Points = 100, FirstName = "Stephen", LastName = "Egnarts", DeckName = "Token Mania" },
                    new ContestantForShallowGame { Points = 0, FirstName = "Peter", LastName = "Rekrap", DeckName = "Free Real Estate" },
                }, false
            };

            yield return new object[]
            {
                new List<ContestantForShallowGame>
                {
                    new ContestantForShallowGame { Points = 300, FirstName = "Tony", LastName = "Stark", DeckName = "Strongsuit"  },
                    new ContestantForShallowGame { Points = 200, FirstName = "Steve", LastName = "Rodgers", DeckName = "Shields Up" },
                    new ContestantForShallowGame { Points = 100, FirstName = "Stephen", LastName = "Strange", DeckName = "Mystical Arts" },
                    new ContestantForShallowGame { Points = 0, FirstName = "Peter", LastName = "Parker", DeckName = "Webbed Up" },
                }, false
            };
        }
    }
}
