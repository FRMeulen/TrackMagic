using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Requests;
using TrackMagic.Application.Common.Searching;
using TrackMagic.Application.Dtos;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.Features.Players.Search
{
    public class SearchPlayerQuery : IQuery<SearchResult<PlayerDto>>
    {
        public List<SearchFilter> Filters { get; set; } = new List<SearchFilter>();
        public SearchOrdering Ordering { get; set; } = default!;
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public class SearchPlayerQueryHandler : IQueryHandler<SearchPlayerQuery, SearchResult<PlayerDto>>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<SearchPlayerQueryHandler> _logger;

        public SearchPlayerQueryHandler(IAppDbContext dbContext, IMapper mapper, ILogger<SearchPlayerQueryHandler> logger)
            => (_appDbContext, _mapper, _logger) = (dbContext, mapper, logger);

        public async Task<SearchResult<PlayerDto>> Handle(SearchPlayerQuery query, CancellationToken cancellationToken)
        {
            /*
             * WIP
             * Filters are not correctly parsed from query
             * Filters are not yet applied
             * Ordering is not yet applied
             */
            var playersQuery = _appDbContext.Set<Player>()
                .AsQueryable();

            var players = await playersQuery
                .Skip(query.PageSize * query.Page)
                .Take(query.PageSize)
                .AsNoTracking()
                .ToListAsync();

            var result = new SearchResult<PlayerDto>
            {
                TotalAmount = await playersQuery.CountAsync(),
                Results = players.Select(x => _mapper.Map<PlayerDto>(x)).ToList(),
            };

            return new SearchResult<PlayerDto>();
        }
    }
}
