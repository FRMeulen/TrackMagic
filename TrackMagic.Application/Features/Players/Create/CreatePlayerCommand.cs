using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Requests;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.Features.Players.Create
{
    public class CreatePlayerCommand : ICommand<int>
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
    }

    public class CreatePlayerCommandHandler : ICommandHandler<CreatePlayerCommand, int>
    {
        private readonly ILogger<CreatePlayerCommandHandler> _logger;
        private readonly IAppDbContext _appDbContext;

        public CreatePlayerCommandHandler(ILogger<CreatePlayerCommandHandler> logger, IAppDbContext dbContext)
            => (_logger, _appDbContext) = (logger, dbContext);

        public async Task<int> Handle(CreatePlayerCommand command, CancellationToken cancellationToken)
        {
            var playerToCreate = new Player
            {
                FirstName = command.FirstName,
                LastName = command.LastName
            };

            _logger.LogInformation($"Creating player {playerToCreate.FullName}.");
            _appDbContext.Set<Player>().Add(playerToCreate);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            var createdPlayer = await _appDbContext.Set<Player>()
                .Where(p => p.FirstName == command.FirstName && p.LastName == command.LastName)
                .FirstAsync(cancellationToken);

            return createdPlayer.Id;
        }
    }
}
