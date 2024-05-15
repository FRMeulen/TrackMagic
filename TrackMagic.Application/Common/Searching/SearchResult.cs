namespace TrackMagic.Application.Common.Searching
{
    public class SearchResult<TDto>
    {
        public int Amount { get; set; }
        public List<TDto> Matches { get; set; } = [];
    }
}
