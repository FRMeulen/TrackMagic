using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Requests;
using TrackMagic.Application.Dtos;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Exceptions;

namespace TrackMagic.Application.Features.Cards.Get
{
    public class GetCardByNameQuery : IQuery<CardDto>
    {
        public string Name { get; set; } = default!;
    }

    public class GetCardByNameQueryHandler : IQueryHandler<GetCardByNameQuery, CardDto>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCardByNameQueryHandler> _logger;

        public GetCardByNameQueryHandler(IAppDbContext dbContext, IMapper mapper, ILogger<GetCardByNameQueryHandler> logger)
            => (_appDbContext, _mapper, _logger) = (dbContext, mapper, logger);

        public async Task<CardDto> Handle(GetCardByNameQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Fetching card {query.Name}.");
            var card = await _appDbContext.Set<Card>()
                .Include(c => c.ColorIdentity)
                .Include(c => c.CommanderOf)
                .Include(c => c.CompanionOf)
                .Include(c => c.Usage)
                    .ThenInclude(dlc => dlc.Decklist)
                        .ThenInclude(dl => dl.Deck)
                .Where(c => c.Name == query.Name)
                .FirstOrDefaultAsync(cancellationToken);

            if (card == null) throw new NotFoundException(nameof(Card));

            return _mapper.Map<CardDto>(card);
        }
    }
}
