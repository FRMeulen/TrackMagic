using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Requests;
using TrackMagic.Application.Dtos;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Constants;
using TrackMagic.Shared.Exceptions;

namespace TrackMagic.Application.Features.Cards.Get
{
    public class GetCardQuery : IQuery<CardDto>
    {
        public int Id { get; set; }
    }

    public class GetCardQueryHandler : IQueryHandler<GetCardQuery, CardDto>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCardQueryHandler> _logger;

        public GetCardQueryHandler(IAppDbContext dbContext, IMapper mapper, ILogger<GetCardQueryHandler> logger)
            => (_appDbContext, _mapper, _logger) = (dbContext, mapper, logger);

        public async Task<CardDto> Handle(GetCardQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Fetching card {query.Id}.");
            var card = await _appDbContext.Set<Card>()
                .Include(c => c.ColorIdentity)
                .Include(c => c.CommanderOf)
                .Include(c => c.CompanionOf)
                .Include(c => c.UsedIn)
                .Where(c => c.Id == query.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (card == null) throw new NotFoundException(DefaultMessages.NotFoundExceptionMessage(nameof(Card)));

            return _mapper.Map<CardDto>(card);
        }
    }
}
