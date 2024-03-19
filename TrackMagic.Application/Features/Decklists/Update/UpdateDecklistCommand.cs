using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Requests;
using TrackMagic.Application.Dtos;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.Features.Decklists.Update
{
    public class UpdateDecklistCommand : ICommand<DecklistDto>
    {
        public int Id { get; set; }
        public List<int> CardIds { get; set; } = default!;
    }

    public class UpdateDecklistCommandHandler : ICommandHandler<UpdateDecklistCommand, DecklistDto>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateDecklistCommandHandler> _logger;

        public UpdateDecklistCommandHandler(IAppDbContext dbContext, IMapper mapper, ILogger<UpdateDecklistCommandHandler> logger)
            => (_appDbContext, _mapper, _logger) = (dbContext, mapper, logger);

        public async Task<DecklistDto> Handle(UpdateDecklistCommand command, CancellationToken cancellationToken)
        {
            var idDictionary = command.CardIds
                .GroupBy(id => id)
                .ToDictionary(group => group.Key, group => group.Count());

            var cards = await _appDbContext.Set<Card>()
                .Where(c => command.CardIds.Contains(c.Id))
                .ToListAsync(cancellationToken);

            var decklistToUpdate = await _appDbContext.Set<Decklist>()
                .Where(dl => dl.Id == command.Id)
                .FirstAsync(cancellationToken);

            var decklistCards = idDictionary
                .Select(id => new DecklistCard
                {
                    Decklist = decklistToUpdate,
                    Card = cards.First(c => c.Id == id.Key),
                    Amount = id.Value
                })
                .ToList();

            decklistToUpdate.Cards = decklistCards;

            _logger.LogInformation("Updating decklist.");
            _appDbContext.Set<Decklist>().Update(decklistToUpdate);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<DecklistDto>(decklistToUpdate);
        }
    }
}
