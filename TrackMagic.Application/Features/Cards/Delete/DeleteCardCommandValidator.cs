using FluentValidation;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Constants;

namespace TrackMagic.Application.Features.Cards.Delete
{
    public class DeleteCardCommandValidator : AbstractValidator<DeleteCardCommand>
    {
        public DeleteCardCommandValidator(ICardsService cardsService)
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                .MustAsync(async (id, cancellationToken)
                    => await cardsService.ExistsAsync(c => c.Id == id, cancellationToken))
                .WithMessage((_) => DefaultMessages.MustExistMessage(nameof(Card), nameof(Card.Id), $"{_.Id}"));
        }
    }
}
