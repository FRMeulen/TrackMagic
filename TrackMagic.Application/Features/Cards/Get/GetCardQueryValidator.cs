using FluentValidation;

namespace TrackMagic.Application.Features.Cards.Get
{
    public class GetCardQueryValidator : AbstractValidator<GetCardQuery>
    {
        public GetCardQueryValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty();
        }
    }
}
