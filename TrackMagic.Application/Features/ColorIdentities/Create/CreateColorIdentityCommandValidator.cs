using FluentValidation;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Constants;

namespace TrackMagic.Application.Features.ColorIdentities.Create
{
    public class CreateColorIdentityCommandValidator : AbstractValidator<CreateColorIdentityCommand>
    {
        public CreateColorIdentityCommandValidator(IColorIdentitiesService colorIdentitiesService)
        {
            RuleFor(ci => ci.Name)
                .NotEmpty()
                .MaximumLength(20)
                .MustAsync(async (name, cancellationToken)
                    => !await colorIdentitiesService.ExistsAsync(ci => ci.Name == name, cancellationToken))
                .WithMessage((_) => DefaultMessages.AlreadyExistsMessage(nameof(ColorIdentity), nameof(ColorIdentity.Name), $"{_.Name}"));

            RuleFor(ci => ci.Colors)
                .NotEmpty();
        }
    }
}
