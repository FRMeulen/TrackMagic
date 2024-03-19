using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Requests;
using TrackMagic.Application.Dtos;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Exceptions;

namespace TrackMagic.Application.Features.Decklists.Get
{
    public class GetDecklistQuery : IQuery<DecklistDto>
    {
        public int Id { get; set; }
    }

    public class GetDecklistQueryHandler : IQueryHandler<GetDecklistQuery, DecklistDto>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDecklistQueryHandler> _logger;

        public GetDecklistQueryHandler(IAppDbContext dbContext, IMapper mapper, ILogger<GetDecklistQueryHandler> logger)
            => (_appDbContext, _mapper, _logger) = (dbContext, mapper, logger);

        public async Task<DecklistDto> Handle(GetDecklistQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Fetching decklist {query.Id}.");
            var decklist = await _appDbContext.Set<Decklist>()
                .Include(dl => dl.Cards)
                    .ThenInclude(c => c.Card)
                .Include(dl => dl.Deck)
                .Where(dl => dl.Id == query.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (decklist == null) throw new NotFoundException(nameof(Decklist));

            return _mapper.Map<DecklistDto>(decklist);
        }
    }
}
