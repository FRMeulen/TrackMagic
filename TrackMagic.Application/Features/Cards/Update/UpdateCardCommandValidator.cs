using FluentValidation;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Constants;

namespace TrackMagic.Application.Features.Cards.Update
{
    public class UpdateCardCommandValidator : AbstractValidator<UpdateCardCommand>
    {
        public UpdateCardCommandValidator(ICardsService cardsService)
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                .MustAsync(async (id, cancellationToken)
                    => await cardsService.ExistsAsync(c => c.Id == id, cancellationToken))
                .WithMessage($"No Card exists with the provided id.");

            RuleFor(c => c.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(c => c.CardTypes)
                .NotEmpty()
                .Must(cts => cts.Count > 0)
                .WithMessage("Card needs at least one card type.");

            RuleFor(c => c)
                .MustAsync(async (command, cancellationToken)
                    => !await cardsService.ExistsAsync(c =>
                        c.Name == command.Name &&
                        c.Id != command.Id,
                        cancellationToken))
                .WithMessage((_) => DefaultMessages.AlreadyExistsMessage(nameof(Card), nameof(Card.Name), $"{_.Name}"))
                .WithName("Duplicate");
        }
    }
}
