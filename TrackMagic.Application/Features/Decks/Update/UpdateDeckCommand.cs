using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Requests;
using TrackMagic.Application.Dtos;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.Features.Decks.Update
{
    public class UpdateDeckCommand : ICommand<DeckDto>
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int OwnerId { get; set; }
        public List<int> CommanderIds { get; set; } = default!;
        public int? CompanionId { get; set; } = null;
        public int DecklistId { get; set; }
    }

    public class UpdateDeckCommandHandler : ICommandHandler<UpdateDeckCommand, DeckDto>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateDeckCommandHandler> _logger;

        public UpdateDeckCommandHandler(IAppDbContext dbContext, IMapper mapper, ILogger<UpdateDeckCommandHandler> logger)
            => (_appDbContext, _mapper, _logger) = (dbContext, mapper, logger);

        public async Task<DeckDto> Handle(UpdateDeckCommand command, CancellationToken cancellationToken)
        {
            var commanders = await _appDbContext.Set<Card>()
                .Include(c => c.CommanderOf)
                .Where(c => command.CommanderIds.Contains(c.Id))
                .ToListAsync(cancellationToken);

            var deckToUpdate = await _appDbContext.Set<Deck>()
                .Include(d => d.Owner)
                .Include(d => d.Commanders)
                .Include(d => d.Companion)
                .Include(d => d.Decklist)
                .Include(d => d.PilotedBy)
                .Where(d => d.Id == command.Id)
                .FirstAsync(cancellationToken);

            deckToUpdate.Name = command.Name;
            deckToUpdate.OwnerId = command.OwnerId;
            deckToUpdate.Commanders = commanders;
            deckToUpdate.CompanionId = command.CompanionId ?? null;
            deckToUpdate.DecklistId = command.DecklistId;

            _logger.LogInformation($"Updating deck {command.Id}.");
            _appDbContext.Set<Deck>().Update(deckToUpdate);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<DeckDto>(deckToUpdate);
        }
    }
}
