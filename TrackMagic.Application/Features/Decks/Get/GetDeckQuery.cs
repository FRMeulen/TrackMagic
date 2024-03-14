using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Requests;
using TrackMagic.Application.Dtos;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Exceptions;

namespace TrackMagic.Application.Features.Decks.Get
{
    public class GetDeckQuery : IQuery<DeckDto>
    {
        public int Id { get; set; }
    }

    public class GetDeckQueryHandler : IQueryHandler<GetDeckQuery, DeckDto>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDeckQueryHandler> _logger;

        public GetDeckQueryHandler(IAppDbContext dbContext, IMapper mapper, ILogger<GetDeckQueryHandler> logger)
            => (_appDbContext, _mapper, _logger) = (dbContext, mapper, logger);

        public async Task<DeckDto> Handle(GetDeckQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Fetching deck {query.Id}.");
            var deck = await _appDbContext.Set<Deck>()
                .Include(d => d.Owner)
                .Include(d => d.Commanders)
                .Include(d => d.Companion)
                .Include(d => d.Decklist)
                .Include(d => d.PilotedBy)
                .Where(d => d.Id == query.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (deck == null) throw new NotFoundException(nameof(Deck));

            return _mapper.Map<DeckDto>(deck);
        }
    }
}
