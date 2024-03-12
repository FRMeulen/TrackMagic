﻿using FluentValidation;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Constants;

namespace TrackMagic.Application.Features.ColorIdentities.Update
{
    public class UpdateColorIdentityCommandValidator : AbstractValidator<UpdateColorIdentityCommand>
    {
        public UpdateColorIdentityCommandValidator(IColorIdentitiesService colorIdentitiesService)
        {
            RuleFor(ci => ci.Id)
                .NotEmpty()
                .MustAsync(async (id, cancellationToken)
                    => await colorIdentitiesService.ExistsAsync(ci => ci.Id == id, cancellationToken))
                .WithMessage((_) => DefaultMessages.MustExistMessage(nameof(ColorIdentity), nameof(ColorIdentity.Id), $"{_.Id}"));

            RuleFor(ci => ci.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(ci => ci.Colors)
                .NotEmpty()
                .MustAsync(async (colors, cancellationToken)
                    => !await colorIdentitiesService.ExistsAsync(ci => ci.Colors.All(colors.Contains) && ci.Colors.Count == colors.Count))
                .WithMessage((_) => DefaultMessages.AlreadyExistsMessage(nameof(ColorIdentity), nameof(ColorIdentity.Colors), $"{_.Colors}"));

            RuleFor(ci => ci)
                .MustAsync(async (command, cancellationToken)
                    => !await colorIdentitiesService.ExistsAsync(ci =>
                        ci.Name == command.Name &&
                        ci.Id != command.Id))
                .WithMessage((_) => DefaultMessages.AlreadyExistsMessage(nameof(ColorIdentity), nameof(ColorIdentity.Name), $"{_.Name}"))
                .WithName("Duplicate");
        }
    }
}
