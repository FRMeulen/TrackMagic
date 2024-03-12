using FluentValidation;

namespace TrackMagic.Application.Features.ColorIdentities.Get
{
    public class GetColorIdentityQueryValidator : AbstractValidator<GetColorIdentityQuery>
    {
        public GetColorIdentityQueryValidator()
        {
            RuleFor(ci => ci.Id)
                .NotEmpty();
        }
    }
}
