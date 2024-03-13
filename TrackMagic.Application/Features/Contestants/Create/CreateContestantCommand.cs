using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Windows.Input;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Requests;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.Features.Contestants.Create
{
    public class CreateContestantCommand : ICommand<int>
    {
        public int Points { get; set; }
        public int GameId { get; set; }
        public int PlayerId { get; set; }
        public int DeckId { get; set; }
    }

    public class CreateContestantCommandHandler : ICommandHandler<CreateContestantCommand, int>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly ILogger<CreateContestantCommandHandler> _logger;

        public CreateContestantCommandHandler(IAppDbContext dbContext, ILogger<CreateContestantCommandHandler> logger)
            => (_appDbContext, _logger) = (dbContext, logger);

        public async Task<int> Handle(CreateContestantCommand command, CancellationToken cancellationToken)
        {
            var contestantToCreate = new Contestant
            {
                Points = command.Points,
                GameId = command.GameId,
                PlayerId = command.PlayerId,
                DeckId = command.DeckId,
            };

            _logger.LogInformation("Creating contestant.");
            _appDbContext.Set<Contestant>().Add(contestantToCreate);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            var createdContestant = await _appDbContext.Set<Contestant>()
                .Where(c => c.GameId == command.GameId)
                .Where(c => c.PlayerId == command.PlayerId)
                .FirstAsync(cancellationToken);

            return createdContestant.Id;
        }
    }
}
