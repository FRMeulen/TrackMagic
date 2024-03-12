using FluentValidation;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Constants;

namespace TrackMagic.Application.Features.Players.Delete
{
    public class DeletePlayerCommandValidator : AbstractValidator<DeletePlayerCommand>
    {
        public DeletePlayerCommandValidator(IPlayersService playersService)
        {
            RuleFor(p => p.Id)
                .NotEmpty()
                .MustAsync(async (id, cancellationToken)
                    => await playersService.ExistsAsync(p => p.Id == id, cancellationToken))
                .WithMessage((_) => DefaultMessages.MustExistMessage(nameof(Player), nameof(_.Id), $"{_.Id}"));
        }
    }
}
