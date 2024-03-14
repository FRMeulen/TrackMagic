using FluentValidation;
using TrackMagic.Shared.Constants;

namespace TrackMagic.Application.Features.Games.Create
{
    public class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>
    {
        public CreateGameCommandValidator()
        {
            RuleFor(g => g.Date)
                .Must(d => d < DateTimeOffset.UtcNow)
                .WithMessage(DefaultMessages.DateInFutureMessage);

            RuleFor(g => g.LengthInCycles)
                .NotEmpty()
                .WithMessage(DefaultMessages.NoTurnZeroMessage);
        }
    }
}
