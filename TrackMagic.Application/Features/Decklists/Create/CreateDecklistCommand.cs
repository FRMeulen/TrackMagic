using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Requests;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.Features.Decklists.Create
{
    public class CreateDecklistCommand : ICommand<int>
    {
        public List<int> CardIds { get; set; } = default!;
    }

    public class CreateDecklistCommandHandler : ICommandHandler<CreateDecklistCommand, int>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly ILogger<CreateDecklistCommandHandler> _logger;

        public CreateDecklistCommandHandler(IAppDbContext dbContext, ILogger<CreateDecklistCommandHandler> logger)
            => (_appDbContext, _logger) = (dbContext, logger);

        public async Task<int> Handle(CreateDecklistCommand command, CancellationToken cancellationToken)
        {
            var idDictionary = command.CardIds
                .GroupBy(id => id)
                .ToDictionary(group => group.Key, group => group.Count());

            var cards = await _appDbContext.Set<Card>()
                .Where(c => command.CardIds.Contains(c.Id))
                .ToListAsync(cancellationToken);

            var decklistToCreate = new Decklist();

            var decklistCards = idDictionary
                .Select(id => new DecklistCard
                {
                    Decklist = decklistToCreate,
                    Card = cards.First(c => c.Id == id.Key),
                    Amount = id.Value
                })
                .ToList();

            decklistToCreate.Cards = decklistCards;

            _logger.LogInformation("Creating decklist.");
            _appDbContext.Set<Decklist>().Add(decklistToCreate);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            var createdDecklist = await _appDbContext.Set<Decklist>()
                .OrderByDescending(dl => dl.Id)
                .FirstAsync(cancellationToken);

            return createdDecklist.Id;
        }
    }
}
