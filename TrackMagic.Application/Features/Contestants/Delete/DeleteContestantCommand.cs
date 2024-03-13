using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Requests;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.Features.Contestants.Delete
{
    public class DeleteContestantCommand : IVoidCommand
    {
        public int Id { get; set; }
    }

    public class DeleteContestantCommandHandler : IVoidCommandHandler<DeleteContestantCommand>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly ILogger<DeleteContestantCommandHandler> _logger;

        public DeleteContestantCommandHandler(IAppDbContext dbContext, ILogger<DeleteContestantCommandHandler> logger)
            => (_appDbContext, _logger) = (dbContext, logger);

        public async Task Handle(DeleteContestantCommand command, CancellationToken cancellationToken)
        {
            var contestantToDelete = await _appDbContext.Set<Contestant>()
                .Where(c => c.Id == command.Id)
                .FirstAsync(cancellationToken);

            _logger.LogInformation($"Deleting contestant {command.Id}.");
            _appDbContext.Set<Contestant>().Remove(contestantToDelete);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
