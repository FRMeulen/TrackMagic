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
        public List<int> Additions { get; set; } = default!;
        public List<int> Removals { get; set; } = default!;
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
            var decklistToUpdate = await _appDbContext.Set<Decklist>()
                .Where(dl => dl.Id == command.Id)
                .FirstAsync(cancellationToken);

            var addedCards = await _appDbContext.Set<Card>()
                .Where(c => command.Additions.Contains(c.Id))
                .ToListAsync(cancellationToken);

            var removedCards = await _appDbContext.Set<Card>()
                .Where(c => command.Removals.Contains(c.Id))
                .ToListAsync(cancellationToken);

            decklistToUpdate.Cards.AddRange(addedCards);
            removedCards.Select(c => decklistToUpdate.Cards.Remove(c));

            _logger.LogInformation($"Updating decklist {command.Id}.");
            _appDbContext.Set<Decklist>().Update(decklistToUpdate);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<DecklistDto>(decklistToUpdate);
        }
    }
}
