using FluentValidation;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Constants;

namespace TrackMagic.Application.Features.Players.Create
{
    public class CreatePlayerCommandValidator : AbstractValidator<CreatePlayerCommand>
    {
        public CreatePlayerCommandValidator(IPlayersService playersService)
        {
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
                        x.LastName == command.LastName,
                        cancellationToken))
                .WithMessage((_) => DefaultMessages.AlreadyExistsMessage(nameof(Player), nameof(Player.FullName), $"{_.FirstName} {_.LastName}"));
        }
    }
}
