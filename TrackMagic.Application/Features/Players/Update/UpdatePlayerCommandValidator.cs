using FluentValidation;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Constants;

namespace TrackMagic.Application.Features.Players.Update
{
    public class UpdatePlayerCommandValidator : AbstractValidator<UpdatePlayerCommand>
    {
        public UpdatePlayerCommandValidator(IPlayersService playersService)
        {
            RuleFor(p => p.Id)
                .NotEmpty()
                .MustAsync(async (id, cancellationToken)
                    => await playersService.ExistsAsync(p => p.Id == id, cancellationToken))
                .WithMessage($"No Player exists with the provided id.");

            RuleFor(p => p.FirstName)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(p => p.LastName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(p => p)
                .MustAsync(async (command, cancellationToken)
                    => !await playersService.ExistsAsync(p =>
                        p.FirstName == command.FirstName &&
                        p.LastName == command.LastName &&
                        p.Id != command.Id,
                        cancellationToken))
                .WithMessage((_) => DefaultMessages.AlreadyExistsMessage(nameof(Player), nameof(Player.FullName), $"{_.FirstName} {_.LastName}"))
                .WithName($"FullName");
        }
    }
}
