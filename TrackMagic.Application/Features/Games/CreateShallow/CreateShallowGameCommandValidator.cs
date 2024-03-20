using FluentValidation;
using TrackMagic.Application.Features.Decks;
using TrackMagic.Application.Features.Players;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Constants;

namespace TrackMagic.Application.Features.Games.CreateShallow
{
    public class CreateShallowGameCommandValidator : AbstractValidator<CreateShallowGameCommand>
    {
        public CreateShallowGameCommandValidator(
            IPlayersService playersService,
            IDecksService decksService)
        {
            RuleForEach(g => g.Contestants)
                .ChildRules(contestant =>
                {
                    contestant.RuleFor(c => c.FirstName)
                        .NotEmpty()
                        .MustAsync(async (name, cancellationToken) =>
                            await playersService.ExistsAsync(p => p.FirstName == name, cancellationToken))
                        .WithMessage((_) => DefaultMessages.MustExistMessage(
                            nameof(Player),
                            nameof(Player.FirstName),
                            $"{_.FirstName}"));

                    contestant.RuleFor(c => c.LastName)
                        .NotEmpty()
                        .MustAsync(async (name, cancellationToken) =>
                            await playersService.ExistsAsync(p => p.LastName == name, cancellationToken))
                        .WithMessage((_) => DefaultMessages.MustExistMessage(
                            nameof(Player),
                            nameof(Player.LastName),
                            $"{_.LastName}"));

                    contestant.RuleFor(c => c.DeckName)
                        .NotEmpty()
                        .MustAsync(async (name, cancellationToken) =>
                            await decksService.ExistsAsync(d => d.Name == name, cancellationToken))
                        .WithMessage((_) => DefaultMessages.MustExistMessage(
                            nameof(Deck),
                            nameof(Deck.Name),
                            $"{_.DeckName}"));
                });
        }
    }
}
