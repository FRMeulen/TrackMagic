using TrackMagic.Application.Common.Requests;
using TrackMagic.Application.Dtos.Base;

namespace TrackMagic.Application.Common.Searching
{
    public class SearchQuery<TDto> : IQuery<SearchResult<TDto>> where TDto : IDto 
    {
        public int? Id { get; set; }
        public bool IsSortAscending { get; set; } = true;
        public string SortBy { get; set; } = "Id"!;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 15;
    }
}
