using TrackMagic.Shared.Enums;

namespace TrackMagic.Application.Common.Searching
{
    public class SearchOrdering
    {
        public string OrderingProperty { get; set; } = default!;
        public OrderingTypes OrderBy { get; set; }
    }
}
