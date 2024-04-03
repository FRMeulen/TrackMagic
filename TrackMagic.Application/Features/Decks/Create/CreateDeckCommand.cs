using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Requests;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.Features.Decks.Create
{
    public class CreateDeckCommand : ICommand<int>
    {
        public string Name { get; set; } = default!;
        public int OwnerId { get; set; }
        public List<int> CommanderIds { get; set; } = default!;
        public int? CompanionId { get; set; }
        public int? DecklistId { get; set; }
    }

    public class CreateDeckCommandHandler : ICommandHandler<CreateDeckCommand, int>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly ILogger<CreateDeckCommandHandler> _logger;

        public CreateDeckCommandHandler(IAppDbContext dbContext, ILogger<CreateDeckCommandHandler> logger)
            => (_appDbContext, _logger) = (dbContext, logger);

        public async Task<int> Handle(CreateDeckCommand command, CancellationToken cancellationToken)
        {
            var commanders = await _appDbContext.Set<Card>()
                .Where(c => command.CommanderIds.Contains(c.Id))
                .ToListAsync(cancellationToken);

            var deckToCreate = new Deck
            {
                Name = command.Name,
                OwnerId = command.OwnerId,
                Commanders = commanders,
                CompanionId = command.CompanionId ?? null,
                DecklistId = command.DecklistId
            };

            _logger.LogInformation($"Creating deck {command.Name}.");
            _appDbContext.Set<Deck>().Add(deckToCreate);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            var createdDeck = await _appDbContext.Set<Deck>()
                .Where(d => d.Name == command.Name)
                .FirstAsync(cancellationToken);

            return createdDeck.Id;
        }
    }
}
