using FluentValidation;

namespace TrackMagic.Application.Features.Players.Search
{
    public class SearchPlayerQueryValidator : AbstractValidator<SearchPlayerQuery>
    {
        public SearchPlayerQueryValidator() 
        {
            RuleFor(p => p.Id)
                .Must(id => id != 0)
                .WithMessage("Id cannot be zero. If not used it should be omitted.");

            RuleFor(p => p.FirstName)
                .MaximumLength(20);

            RuleFor(p => p.LastName)
                .MaximumLength(50);

            RuleFor(p => p.Page)
                .NotEmpty();

            RuleFor(p => p.PageSize)
                .Must(size => size > 0);

            RuleFor(p => p.SortBy)
                .Must(by => by.Equals("Id") || by.Equals("FirstName") || by.Equals("LastName"))
                .WithMessage("SortBy needs a valid property name.");
        }
    }
}
