using FluentValidation;
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

            RuleFor(ci => ci)
                .MustAsync(async (command, cancellationToken)
                    => !await colorIdentitiesService.ExistsAsync(ci =>
                        ci.Name == command.Name &&
                        ci.Id != command.Id,
                        cancellationToken))
                .WithMessage((_) => DefaultMessages.AlreadyExistsMessage(nameof(ColorIdentity), nameof(ColorIdentity.Name), $"{_.Name}"))
                .WithName("Duplicate");
        }
    }
}
