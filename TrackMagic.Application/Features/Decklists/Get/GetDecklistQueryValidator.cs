using FluentValidation;

namespace TrackMagic.Application.Features.Decklists.Get
{
    public class GetDecklistQueryValidator : AbstractValidator<GetDecklistQuery>
    {
        public GetDecklistQueryValidator()
        {
            RuleFor(dl => dl.Id)
                .NotEmpty();
        }
    }
}
