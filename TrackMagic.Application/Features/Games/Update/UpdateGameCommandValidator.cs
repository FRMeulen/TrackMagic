using FluentValidation;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Constants;

namespace TrackMagic.Application.Features.Games.Update
{
    public class UpdateGameCommandValidator : AbstractValidator<UpdateGameCommand>
    {
        public UpdateGameCommandValidator(IGamesService gamesService)
        {
            RuleFor(g => g.Id)
                .NotEmpty()
                .MustAsync(async (id, cancellationToken)
                    => await gamesService.ExistsAsync(g => g.Id == id, cancellationToken))
                .WithMessage((_) => DefaultMessages.MustExistMessage(nameof(Game), nameof(Game.Id), $"{_.Id}"));

            RuleFor(g => g.Date)
                .Must(d => d == null || d < DateTimeOffset.UtcNow)
                .WithMessage(DefaultMessages.DateInFutureMessage);

            RuleFor(g => g.LengthInCycles)
                .NotEmpty()
                .WithMessage(DefaultMessages.NoTurnZeroMessage);
        }
    }
}
