using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Requests;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Enums;

namespace TrackMagic.Application.Features.Games.CreateShallow
{
    public class CreateShallowGameCommand : ICommand<int>
    {
        public DateTimeOffset? Date { get; set; } = default!;
        public int LengthInCycles { get; set; }
        public GameTypes GameType { get; set; }
        public List<ContestantForShallowGame> Contestants { get; set; } = default!;
    }

    public class CreateShallowGameCommandHandler : ICommandHandler<CreateShallowGameCommand, int>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly ILogger<CreateShallowGameCommandHandler> _logger;

        public CreateShallowGameCommandHandler(IAppDbContext dbContext, ILogger<CreateShallowGameCommandHandler> logger)
            => (_appDbContext, _logger) = (dbContext, logger);

        public async Task<int> Handle(CreateShallowGameCommand command, CancellationToken cancellationToken)
        {
            var gameToCreate = new Game
            {
                Date = command.Date ?? DateTimeOffset.UtcNow,
                LengthInCycles = command.LengthInCycles,
                GameType = command.GameType,
                Contestants = new List<Contestant>()
            };

            command.Contestants.Select(async c => gameToCreate.Contestants.Add(await ProcessContestant(c, cancellationToken)));

            _logger.LogInformation($"Created game on {command.Date} for {command.Contestants.Count} contestants.");
            _appDbContext.Set<Game>().Add(gameToCreate);

            _appDbContext.ChangeTracker.DetectChanges();
            var yeet = _appDbContext.ChangeTracker.DebugView.ShortView;

            await _appDbContext.SaveChangesAsync(cancellationToken);

            var createdGame = await _appDbContext.Set<Game>()
                .OrderByDescending(g => g.Id)
                .FirstAsync(cancellationToken);

            return createdGame.Id;
        }

        private async Task<Contestant> ProcessContestant(ContestantForShallowGame contestant, CancellationToken cancellationToken)
        {
            var player = await _appDbContext.Set<Player>()
                .Where(p => p.FirstName == contestant.FirstName &&
                            p.LastName == contestant.LastName)
                .FirstAsync(cancellationToken);

            var deck = await _appDbContext.Set<Deck>()
                .Where(d => d.Name == contestant.DeckName)
                .FirstAsync(cancellationToken);

            return new Contestant
            {
                Points = contestant.Points,
                Player = player,
                Deck = deck
            };
        }
    }

    public class ContestantForShallowGame
    {
        public int Points { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string DeckName { get; set; } = default!;
    }
}
