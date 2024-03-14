using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Requests;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.Features.Decks.Delete
{
    public class DeleteDeckCommand : IVoidCommand
    {
        public int Id { get; set; }
    }

    public class DeleteDeckCommandHandler : IVoidCommandHandler<DeleteDeckCommand>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly ILogger<DeleteDeckCommandHandler> _logger;

        public DeleteDeckCommandHandler(IAppDbContext dbContext, ILogger<DeleteDeckCommandHandler> logger)
            => (_appDbContext, _logger) = (dbContext, logger);

        public async Task Handle(DeleteDeckCommand command, CancellationToken cancellationToken)
        {
            var deckToDelete = await _appDbContext.Set<Deck>()
                .Where(d => d.Id == command.Id)
                .FirstAsync(cancellationToken);

            _logger.LogInformation($"Deleting deck {command.Id}.");
            _appDbContext.Set<Deck>().Remove(deckToDelete);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
