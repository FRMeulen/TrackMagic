using FluentValidation;
using TrackMagic.Application.Features.Cards;
using TrackMagic.Application.Features.Decklists;
using TrackMagic.Application.Features.Players;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Constants;

namespace TrackMagic.Application.Features.Decks.Create
{
    public class CreateDeckCommandValidator : AbstractValidator<CreateDeckCommand>
    {
        public CreateDeckCommandValidator(
            IDecksService decksService,
            ICardsService cardsService,
            IPlayersService playersService,
            IDecklistsService decklistsService)
        {
            RuleFor(d => d.Name)
                .NotEmpty()
                .MustAsync(async (name, cancellationToken)
                    => !await decksService.ExistsAsync(d => d.Name == name, cancellationToken))
                .WithMessage((_) => DefaultMessages.AlreadyExistsMessage(nameof(Deck), nameof(Deck.Name), $"{_.Name}"));

            RuleFor(d => d.OwnerId)
                .NotEmpty()
                .MustAsync(async (id, cancellationToken)
                    => await playersService.ExistsAsync(p => p.Id == id, cancellationToken))
                .WithMessage((_) => DefaultMessages.MustExistMessage(nameof(Player), nameof(Player.Id), $"{_.OwnerId}"));

            RuleForEach(d => d.CommanderIds)
                .ChildRules(id =>
                {
                    id.RuleFor(id => id)
                        .NotEmpty()
                        .MustAsync(async (id, cancellationToken) =>
                            await cardsService.ExistsAsync(c => c.Id == id, cancellationToken))
                        .WithMessage((_) => DefaultMessages.MustExistMessage(nameof(Card), nameof(Card.Id), $"{id}"));
                });

            RuleFor(d => d.CompanionId)
                .MustAsync(async (id, cancellationToken)
                    => id == null || await cardsService.ExistsAsync(c => c.Id == id, cancellationToken))
                .WithMessage((_) => DefaultMessages.MustExistMessage(nameof(Card), nameof(Card.Id), $"{_.CompanionId}"));

            RuleFor(d => d.DecklistId)
                .NotEmpty()
                .MustAsync(async (id, cancellationToken)
                    => await decklistsService.ExistsAsync(dl => dl.Id == id, cancellationToken))
                .WithMessage((_) => DefaultMessages.MustExistMessage(nameof(Decklist), nameof(Decklist.Id), $"{_.DecklistId}"));
        }
    }
}
