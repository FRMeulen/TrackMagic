namespace TrackMagic.Application.Common.Searching
{
    public class SearchResult<TDto>
    {
        public int TotalAmount { get; set; }
        public List<TDto> Results { get; set; } = new List<TDto>();
    }
}
