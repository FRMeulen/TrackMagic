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
                .WithMessage(DefaultMessages.FullDecklistMessage);

            RuleForEach(dl => dl.CardIds)
                .ChildRules(id =>
                {
                    id.RuleFor(id => id)
                        .NotEmpty()
                        .MustAsync(async (id, cancellationToken) =>
                            await cardsService.ExistsAsync(c => c.Id == id, cancellationToken))
                        .WithMessage((_) => DefaultMessages.MustExistMessage(nameof(Card), nameof(Card.Id), $"{id}"));
                });
        }
    }
}
