using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Requests;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.Features.Games.Delete
{
    public class DeleteGameCommand : IVoidCommand
    {
        public int Id { get; set; }
    }

    public class DeleteGameCommandHandler : IVoidCommandHandler<DeleteGameCommand>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly ILogger<DeleteGameCommandHandler> _logger;

        public DeleteGameCommandHandler(IAppDbContext dbContext, ILogger<DeleteGameCommandHandler> logger)
            => (_appDbContext, _logger) = (dbContext, logger);

        public async Task Handle(DeleteGameCommand command, CancellationToken cancellationToken)
        {
            var gameToDelete = await _appDbContext.Set<Game>()
                .Where(g => g.Id == command.Id)
                .FirstAsync(cancellationToken);

            _logger.LogInformation($"Deleting game {command.Id}.");
            _appDbContext.Set<Game>().Remove(gameToDelete);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
