﻿using FluentValidation;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Constants;

namespace TrackMagic.Application.Features.Games.GetDetailed
{
    public class GetDetailedGameQueryValidator : AbstractValidator<GetDetailedGameQuery>
    {
        public GetDetailedGameQueryValidator(IGamesService gamesService)
        {
            RuleFor(g => g.Id)
                .NotEmpty()
                .MustAsync(async (id, cancellationToken)
                    => await gamesService.ExistsAsync(g => g.Id == id, cancellationToken))
                .WithMessage((_) => DefaultMessages.MustExistMessage(nameof(Game), nameof(Game.Id), $"{_.Id}"));
        }
    }
}
