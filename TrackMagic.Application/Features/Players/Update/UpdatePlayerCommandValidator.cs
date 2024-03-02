using FluentValidation;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Constants;

namespace TrackMagic.Application.Features.Players.Update
{
    public class UpdatePlayerCommandValidator : AbstractValidator<UpdatePlayerCommand>
    {
        public UpdatePlayerCommandValidator(IPlayersService playersService)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .GreaterThan(0)
                .MustAsync(async (id, cancellationToken)
                    => await playersService.ExistsAsync(x => x.Id == id, cancellationToken))
                .WithMessage((_) => $"No Player exists with the provided id.");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x)
                .MustAsync(async (command, cancellationToken)
                    => !await playersService.ExistsAsync(x =>
                        x.FirstName == command.FirstName &&
                        x.LastName == command.LastName &&
                        x.Id != command.Id,
                        cancellationToken))
                .WithMessage((_) => DefaultMessages.AlreadyExistsMessage(nameof(Player), nameof(Player.FullName), $"{_.FirstName} {_.LastName}"))
                .WithName($"FullName");
        }
    }
}
