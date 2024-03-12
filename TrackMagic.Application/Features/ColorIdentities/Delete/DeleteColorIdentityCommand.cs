using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Requests;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.Features.ColorIdentities.Delete
{
    public class DeleteColorIdentityCommand : IVoidCommand
    {
        public int Id { get; set; }
    }

    public class DeleteColorIdentityCommandHandler : IVoidCommandHandler<DeleteColorIdentityCommand>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly ILogger<DeleteColorIdentityCommandHandler> _logger;

        public DeleteColorIdentityCommandHandler(IAppDbContext dbContext, ILogger<DeleteColorIdentityCommandHandler> logger)
            => (_appDbContext, _logger) = (dbContext, logger);

        public async Task Handle(DeleteColorIdentityCommand command, CancellationToken cancellationToken)
        {
            var colorIdentityToDelete = await _appDbContext.Set<ColorIdentity>()
                .Where(ci => ci.Id == command.Id)
                .FirstAsync(cancellationToken);

            _logger.LogInformation($"Deleting color identity {command.Id}.");
            _appDbContext.Set<ColorIdentity>().Remove(colorIdentityToDelete);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
