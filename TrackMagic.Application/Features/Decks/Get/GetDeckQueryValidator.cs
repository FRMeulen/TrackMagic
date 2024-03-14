using FluentValidation;

namespace TrackMagic.Application.Features.Decks.Get
{
    public class GetDeckQueryValidator : AbstractValidator<GetDeckQuery>
    {
        public GetDeckQueryValidator()
        {
            RuleFor(d => d.Id)
                .NotEmpty();
        }
    }
}
