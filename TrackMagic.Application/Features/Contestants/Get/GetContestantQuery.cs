using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Requests;
using TrackMagic.Application.Dtos;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Exceptions;

namespace TrackMagic.Application.Features.Contestants.Get
{
    public class GetContestantQuery : IQuery<ContestantDto>
    {
        public int Id { get; set; }
    }

    public class GetContestantQueryHandler : IQueryHandler<GetContestantQuery, ContestantDto>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<GetContestantQueryHandler> _logger;

        public GetContestantQueryHandler(IAppDbContext dbContext, IMapper mapper, ILogger<GetContestantQueryHandler> logger)
            => (_appDbContext, _mapper, _logger) = (dbContext, mapper, logger);

        public async Task<ContestantDto> Handle(GetContestantQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Fetching contestant {query.Id}.");
            var contestant = await _appDbContext.Set<Contestant>()
                .Include(c => c.Game)
                .Include(c => c.Player)
                .Include(c => c.Deck)
                .Where(c => c.Id == query.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (contestant == null) throw new NotFoundException(nameof(Contestant));

            return _mapper.Map<ContestantDto>(contestant);
        }
    }
}
