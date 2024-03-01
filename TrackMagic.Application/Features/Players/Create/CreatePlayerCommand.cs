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

        public async Task<int> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
        {
            var playerToCreate = new Player
            {
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            _logger.LogInformation($"Creating player {playerToCreate.FullName}.");
            _appDbContext.Set<Player>().Add(playerToCreate);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            var createdPlayer = await _appDbContext.Set<Player>()
                .Where(x => x.FirstName == request.FirstName && x.LastName == request.LastName)
                .FirstAsync();

            return createdPlayer.Id;
        }
    }
}
