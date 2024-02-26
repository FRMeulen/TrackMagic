using FluentValidation;

namespace TrackMagic.Application.Features.Players.Get
{
    public class GetPlayerQueryValidator : AbstractValidator<GetPlayerQuery>
    {
        public GetPlayerQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
