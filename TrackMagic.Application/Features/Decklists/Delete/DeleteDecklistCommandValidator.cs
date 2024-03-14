using FluentValidation;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Constants;

namespace TrackMagic.Application.Features.Decklists.Delete
{
    public class DeleteDecklistCommandValidator : AbstractValidator<DeleteDecklistCommand>
    {
        public DeleteDecklistCommandValidator(IDecklistsService decklistsService)
        {
            RuleFor(dl => dl.Id)
                .NotEmpty()
                .MustAsync(async (id, cancellationToken)
                    => await decklistsService.ExistsAsync(dl => dl.Id == id, cancellationToken))
                .WithMessage((_) => DefaultMessages.MustExistMessage(nameof(Decklist), nameof(Decklist.Id), $"{_.Id}"));
        }
    }
}
