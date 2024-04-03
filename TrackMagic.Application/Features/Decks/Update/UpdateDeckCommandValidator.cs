using FluentValidation;
using TrackMagic.Application.Features.Cards;
using TrackMagic.Application.Features.Decklists;
using TrackMagic.Application.Features.Players;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Constants;

namespace TrackMagic.Application.Features.Decks.Update
{
    public class UpdateDeckCommandValidator : AbstractValidator<UpdateDeckCommand>
    {
        public UpdateDeckCommandValidator(
            IDecksService decksService,
            IPlayersService playersService,
            ICardsService cardsService,
            IDecklistsService decklistsService)
        {
            RuleFor(d => d.Id)
                .NotEmpty()
                .MustAsync(async (id, cancellationToken)
                    => await decksService.ExistsAsync(d => d.Id == id, cancellationToken))
                .WithMessage((_) => DefaultMessages.MustExistMessage(nameof(Deck), nameof(Deck.Id), $"{_.Id}"));

            RuleFor(d => d.Name)
                .NotEmpty();

            RuleFor(d => d.OwnerId)
                .NotEmpty()
                .MustAsync(async (id, cancellationToken)
                    => await playersService.ExistsAsync(p => p.Id == id, cancellationToken))
                .WithMessage((_) => DefaultMessages.MustExistMessage(nameof(Player), nameof(Player.Id), $"{_.OwnerId}"));

            RuleFor(d => d.CommanderIds)
                .NotEmpty();
            
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
                .MustAsync(async (id, cancellationToken)
                    => id == null || await decklistsService.ExistsAsync(dl => dl.Id == id, cancellationToken))
                .WithMessage((_) => DefaultMessages.MustExistMessage(nameof(Decklist), nameof(Decklist.Id), $"{_.DecklistId}"));


            RuleFor(d => d)
                .MustAsync(async (command, cancellationToken)
                    => !await decksService.ExistsAsync(d =>
                        d.Name == command.Name &&
                        d.Id != command.Id,
                        cancellationToken))
                .WithMessage((_) => DefaultMessages.AlreadyExistsMessage(nameof(Deck), nameof(Deck.Name), $"{_.Name}"));
        }
    }
}
