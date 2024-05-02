using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Requests;
using TrackMagic.Application.Dtos;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Exceptions;

namespace TrackMagic.Application.Features.Games.GetDetailed
{
    public class GetDetailedGameQuery : ICommand<DetailedGameDto>
    {
        public int Id { get; set; }
    }

    public class GetDetailedGameCommandHandler : ICommandHandler<GetDetailedGameQuery, DetailedGameDto>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDetailedGameCommandHandler> _logger;

        public GetDetailedGameCommandHandler(IAppDbContext dbContext, IMapper mapper, ILogger<GetDetailedGameCommandHandler> logger)
            => (_appDbContext, _mapper, _logger) = (dbContext, mapper, logger);

        public async Task<DetailedGameDto> Handle(GetDetailedGameQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Fetching detailed game {query.Id}.");
            var game = await _appDbContext.Set<Game>()
                .Include(g => g.Contestants)
                    .ThenInclude(c => c.Player)
                .Include(g => g.Contestants)
                    .ThenInclude(c => c.Deck)
                        .ThenInclude(d => d.Commanders)
                            .ThenInclude(c => c.ColorIdentity)
                .Include(g => g.Contestants)
                    .ThenInclude(c => c.Deck)
                        .ThenInclude(d => d.Companion)
                            .ThenInclude(c => c.ColorIdentity)
                .Include(g => g.Contestants)
                    .ThenInclude(c => c.Deck)
                        .ThenInclude(d => d.Decklist)
                            .ThenInclude(dl => dl.Cards)
                                .ThenInclude(dlc => dlc.Card)
                                    .ThenInclude(c => c.ColorIdentity)
                .Include(g => g.Contestants)
                    .ThenInclude(c => c.Deck)
                        .ThenInclude(d => d.Owner)
                .Include(g => g.Contestants)
                    .ThenInclude(c => c.Player)
                .Where(g => g.Id == query.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (game == null) throw new NotFoundException(nameof(Game));

            return _mapper.Map<DetailedGameDto>(game);
        }
    }
}
