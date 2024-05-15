using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TrackMagic.Application.Dtos.Base;
using TrackMagic.Domain.Entities.Base;

namespace TrackMagic.Application.Common.Searching
{
    public static class SearchingExtensions
    {
        public static IQueryable<TEntity> ApplyOrdering<TEntity, TDto>(this IQueryable<TEntity> query, SearchQuery<TDto> searchQuery,
            Dictionary<string, Expression<Func<TEntity, object>>> columnsMap)
            where TEntity : BaseEntity
            where TDto : IDto
        {
            if (string.IsNullOrWhiteSpace(searchQuery.SortBy) || !columnsMap.ContainsKey(searchQuery.SortBy)) return query;

            return searchQuery.IsSortAscending
                ? query.OrderBy(columnsMap[searchQuery.SortBy])
                : query.OrderByDescending(columnsMap[searchQuery.SortBy]);
        }

        public static async Task<SearchResult<TDto>> ToSearchResultAsync<TDto>(
            this IQueryable<TDto> query,
            CancellationToken cancellationToken = default)
            where TDto : IDto
        {
            return new SearchResult<TDto>
            {
                Amount = query.Count(),
                Matches = await query.ToListAsync(cancellationToken),
            };
        }
    }
}
