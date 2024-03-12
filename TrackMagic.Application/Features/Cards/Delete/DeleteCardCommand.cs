using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Requests;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.Features.Cards.Delete
{
    public class DeleteCardCommand : IVoidCommand
    {
        public int Id { get; set; }
    }

    public class DeleteCardCommandHandler : IVoidCommandHandler<DeleteCardCommand>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly ILogger<DeleteCardCommandHandler> _logger;

        public DeleteCardCommandHandler(IAppDbContext dbContext, ILogger<DeleteCardCommandHandler> logger)
            => (_appDbContext, _logger) = (dbContext, logger);

        public async Task Handle(DeleteCardCommand command, CancellationToken cancellationToken)
        {
            var cardToDelete = await _appDbContext.Set<Card>()
                .Where(c => c.Id == command.Id)
                .FirstAsync(cancellationToken);

            _logger.LogInformation($"Deleting card {cardToDelete.Name}.");
            _appDbContext.Set<Card>().Remove(cardToDelete);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
