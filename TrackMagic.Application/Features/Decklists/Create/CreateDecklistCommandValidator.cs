using FluentValidation;
using TrackMagic.Application.Features.Cards;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Constants;

namespace TrackMagic.Application.Features.Decklists.Create
{
    public class CreateDecklistCommandValidator : AbstractValidator<CreateDecklistCommand>
    {
        public CreateDecklistCommandValidator(ICardsService cardsService)
        {
            RuleFor(dl => dl.CardIds)
                .NotEmpty()
                .Must(ids => ids.Count == 100)
                .MustAsync(async (ids, cancellationToken)
                    => await cardsService.AllExistAsync(ids, cancellationToken))
                .WithMessage((_) => DefaultMessages.MustExistMessage(nameof(Card), nameof(Card.Id), $"from list"));
        }
    }
}
