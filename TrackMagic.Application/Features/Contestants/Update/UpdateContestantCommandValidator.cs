using FluentValidation;
using TrackMagic.Application.Features.Decks;
using TrackMagic.Application.Features.Games;
using TrackMagic.Application.Features.Players;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Constants;

namespace TrackMagic.Application.Features.Contestants.Update
{
    public class UpdateContestantCommandValidator : AbstractValidator<UpdateContestantCommand>
    {
        public UpdateContestantCommandValidator(
            IContestantsService contestantsService,
            IGamesService gamesService,
            IPlayersService playersService,
            IDecksService decksService) 
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                .MustAsync(async (id, cancellationToken)
                    => await contestantsService.ExistsAsync(c => c.Id == id, cancellationToken))
                .WithMessage((_) => DefaultMessages.MustExistMessage(nameof(Contestant), nameof(Contestant.Id), $"{_.Id}"));

            RuleFor(c => c.GameId)
                .NotEmpty()
                .MustAsync(async (id, cancellationToken)
                    => await gamesService.ExistsAsync(g => g.Id == id, cancellationToken))
                .WithMessage((_) => DefaultMessages.MustExistMessage(nameof(Game), nameof(Game.Id), $"{_.Id}"));

            RuleFor(c => c.PlayerId)
                .NotEmpty()
                .MustAsync(async (id, cancellationToken)
                    => await playersService.ExistsAsync(p => p.Id == id, cancellationToken))
                .WithMessage((_) => DefaultMessages.MustExistMessage(nameof(Player), nameof(Player.Id), $"{_.Id}"));

            RuleFor(c => c.DeckId)
                .NotEmpty()
                .MustAsync(async (id, cancellationToken)
                    => await decksService.ExistsAsync(d => d.Id == id, cancellationToken))
                .WithMessage((_) => DefaultMessages.MustExistMessage(nameof(Deck), nameof(Deck.Id), $"{_.Id}"));
        }
    }
}
