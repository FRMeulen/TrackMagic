using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Requests;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.Features.Decklists.Delete
{
    public class DeleteDecklistCommand : IVoidCommand
    {
        public int Id { get; set; }
    }

    public class DeleteDecklistCommandHandler : IVoidCommandHandler<DeleteDecklistCommand>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly ILogger<DeleteDecklistCommandHandler> _logger;

        public DeleteDecklistCommandHandler(IAppDbContext dbContext, ILogger<DeleteDecklistCommandHandler> logger)
            => (_appDbContext, _logger) = (dbContext, logger);

        public async Task Handle(DeleteDecklistCommand command, CancellationToken cancellationToken)
        {
            var decklistToDelete = await _appDbContext.Set<Decklist>()
                .Where(dl => dl.Id == command.Id)
                .FirstAsync(cancellationToken);

            _logger.LogInformation($"Deleting decklist {command.Id}.");
            _appDbContext.Set<Decklist>().Remove(decklistToDelete);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
