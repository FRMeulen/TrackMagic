using FluentValidation;

namespace TrackMagic.Application.Features.Players.Get
{
    public class GetPlayerQueryValidator : AbstractValidator<GetPlayerQuery>
    {
        public GetPlayerQueryValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty();
        }
    }
}
