using FluentValidation;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Constants;

namespace TrackMagic.Application.Features.Cards.Create
{
    public class CreateCardCommandValidator : AbstractValidator<CreateCardCommand>
    {
        public CreateCardCommandValidator(ICardsService cardsService)
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .MaximumLength(50)
                .MustAsync(async (name, cancellationToken)
                    => !await cardsService.ExistsAsync(c => c.Name == name, cancellationToken))
                .WithMessage((_) => DefaultMessages.AlreadyExistsMessage(nameof(Card), nameof(Card.Name), $"{_.Name}"));

            RuleFor(c => c.CardTypes)
                .NotEmpty()
                .Must(cts => cts.Count > 0)
                .WithMessage("Card needs at least one card type.");

            RuleFor(c => c.ColorIdentityId)
                .NotEmpty();
        }
    }
}
