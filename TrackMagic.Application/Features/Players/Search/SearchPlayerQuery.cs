using AutoMapper;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Requests;
using TrackMagic.Application.Common.Searching;
using TrackMagic.Application.Dtos;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.Features.Players.Search
{
    public class SearchPlayerQuery : SearchQuery<PlayerDto>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
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
            var logString = $"Searching Player";
            var predicate = PredicateBuilder.New<Player>(true);
            if (query.Id is not null)
            {
                predicate = predicate.And(p => p.Id == query.Id);
                logString += $" with id {query.Id}";
            }

            if (!string.IsNullOrWhiteSpace(query.FirstName))
            {
                predicate = predicate.And(p => p.FirstName.Contains(query.FirstName));
                logString += $" with first name {query.FirstName}";
            }

            if (!string.IsNullOrWhiteSpace(query.LastName))
            {
                predicate = predicate.And(p => p.LastName.Contains(query.LastName));
                logString += $" with last name {query.LastName}";
            }

            _logger.LogInformation($"{logString}.");
            var result = await _appDbContext.Set<Player>()
                .Include(p => p.Contested)
                .Include(p => p.Decks)
                .Where(predicate)
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .AsNoTracking()
                .ApplyOrdering(query, _columnsMap)
                .Select(p => _mapper.Map<PlayerDto>(p))
                .ToSearchResultAsync();

            _logger.LogInformation($"Found {result.Amount} matches.");
            return result;
        }

        private readonly Dictionary<string, Expression<Func<Player, object>>> _columnsMap = new Dictionary<string, Expression<Func<Player, object>>>
        {
            ["Id"] = p => p.Id,
            ["FirstName"] = p => p.FirstName,
            ["LastName"] = p => p.LastName,
        };
    }
}
