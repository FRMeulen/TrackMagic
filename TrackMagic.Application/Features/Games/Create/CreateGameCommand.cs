using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Requests;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Enums;

namespace TrackMagic.Application.Features.Games.Create
{
    public class CreateGameCommand : ICommand<int>
    {
        public DateTimeOffset? Date { get; set; }
        public int LengthInCycles { get; set; }
        public GameTypes GameType { get; set; }
    }

    public class CreateGameCommandHandler : ICommandHandler<CreateGameCommand, int>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly ILogger<CreateGameCommandHandler> _logger;

        public CreateGameCommandHandler(IAppDbContext dbContext, ILogger<CreateGameCommandHandler> logger)
            => (_appDbContext, _logger) = (dbContext, logger);

        public async Task<int> Handle(CreateGameCommand command, CancellationToken cancellationToken)
        {
            var gameToCreate = new Game
            {
                Date = command.Date ?? DateTimeOffset.UtcNow,
                LengthInCycles = command.LengthInCycles,
                GameType = command.GameType,
            };

            _logger.LogInformation($"Creating game.");
            _appDbContext.Set<Game>().Add(gameToCreate);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            var createdGame = await _appDbContext.Set<Game>()
                .OrderByDescending(g => g.Id)
                .FirstAsync(cancellationToken);

            return createdGame.Id;
        }
    }
}
