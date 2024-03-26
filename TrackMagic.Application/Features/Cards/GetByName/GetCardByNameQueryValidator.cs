using FluentValidation;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Constants;

namespace TrackMagic.Application.Features.Cards.GetByName
{
    public class GetCardByNameQueryValidator : AbstractValidator<GetCardByNameQuery>
    {
        public GetCardByNameQueryValidator(ICardsService cardsService)
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .MustAsync(async (name, cancellationToken)
                    => await cardsService.ExistsAsync(c => c.Name == name, cancellationToken))
                .WithMessage((_) => DefaultMessages.MustExistMessage(nameof(Card), nameof(Card.Name), $"{_.Name}"));
        }
    }
}
