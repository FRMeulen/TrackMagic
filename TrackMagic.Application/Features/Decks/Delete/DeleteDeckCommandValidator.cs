using FluentValidation;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Constants;

namespace TrackMagic.Application.Features.Decks.Delete
{
    public class DeleteDeckCommandValidator : AbstractValidator<DeleteDeckCommand>
    {
        public DeleteDeckCommandValidator(IDecksService decksService)
        {
            RuleFor(d => d.Id)
                .NotEmpty()
                .MustAsync(async (id, cancellationToken)
                    => await decksService.ExistsAsync(d => d.Id == id, cancellationToken))
                .WithMessage((_) => DefaultMessages.MustExistMessage(nameof(Deck), nameof(Deck.Id), $"{_.Id}"));
        }
    }
}
