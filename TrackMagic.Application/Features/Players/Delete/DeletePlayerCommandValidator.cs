using FluentValidation;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Constants;

namespace TrackMagic.Application.Features.Players.Delete
{
    public class DeletePlayerCommandValidator : AbstractValidator<DeletePlayerCommand>
    {
        public DeletePlayerCommandValidator(IPlayersService playersService)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .GreaterThan(0)
                .MustAsync(async (id, cancellationToken)
                    => await playersService.ExistsAsync(x => x.Id == id))
                .WithMessage((_) => DefaultMessages.MustExistMessage(nameof(Player), nameof(_.Id), $"{_.Id}"));
        }
    }
}
