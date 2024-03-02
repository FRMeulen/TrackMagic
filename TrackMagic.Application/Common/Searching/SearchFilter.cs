using TrackMagic.Shared.Enums;

namespace TrackMagic.Application.Common.Searching
{
    public class SearchFilter
    {
        public string FilterProperty { get; set; } = default!;
        public ConstraintTypes Constraint { get; set; }
        public string FilterValue { get; set; } = default!;
    }
}
