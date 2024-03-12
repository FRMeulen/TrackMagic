using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Requests;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.Features.Players.Delete
{
    public class DeletePlayerCommand : IVoidCommand
    {
        public int Id { get; set; }
    }

    public class DeletePlayerCommandHandler : IVoidCommandHandler<DeletePlayerCommand>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly ILogger<DeletePlayerCommandHandler> _logger;

        public DeletePlayerCommandHandler(IAppDbContext dbContext, ILogger<DeletePlayerCommandHandler> logger)
            => (_appDbContext, _logger) = (dbContext, logger);

        public async Task Handle(DeletePlayerCommand command, CancellationToken cancellationToken)
        {
            var playerToDelete = await _appDbContext.Set<Player>()
                .Where(p => p.Id == command.Id)
                .FirstAsync(cancellationToken);

            _logger.LogInformation($"Deleting player {playerToDelete.FullName}");
            _appDbContext.Set<Player>().Remove(playerToDelete);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
