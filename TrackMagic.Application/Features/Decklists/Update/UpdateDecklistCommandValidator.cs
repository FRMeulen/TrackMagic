using FluentValidation;
using TrackMagic.Application.Features.Cards;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Constants;

namespace TrackMagic.Application.Features.Decklists.Update
{
    public class UpdateDecklistCommandValidator : AbstractValidator<UpdateDecklistCommand>
    {
        public UpdateDecklistCommandValidator(IDecklistsService decklistsService, ICardsService cardsService)
        {
            RuleFor(dl => dl.Id)
                .NotEmpty()
                .MustAsync(async (id, cancellationToken)
                    => await decklistsService.ExistsAsync(dl => dl.Id == id, cancellationToken))
                .WithMessage((_) => DefaultMessages.MustExistMessage(nameof(Decklist), nameof(Decklist.Id), $"{_.Id}"));

            RuleForEach(dl => dl.CardIds)
                .ChildRules(id =>
                {
                    id.RuleFor(id => id)
                        .NotEmpty()
                        .MustAsync(async (id, cancellationToken) =>
                            await cardsService.ExistsAsync(c => c.Id == id, cancellationToken))
                        .WithMessage((_) => DefaultMessages.MustExistMessage(nameof(Card), nameof(Card.Id), $"{id}"));
                });

            RuleFor(dl => dl)
                .Must(dl => dl.CardIds.Count == 100)
                .WithMessage(DefaultMessages.FullDecklistMessage);
        }
    }
}
