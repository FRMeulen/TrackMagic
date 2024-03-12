using FluentValidation;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Constants;

namespace TrackMagic.Application.Features.ColorIdentities.Delete
{
    public class DeleteColorIdentityCommandValidator : AbstractValidator<DeleteColorIdentityCommand>
    {
        public DeleteColorIdentityCommandValidator(IColorIdentitiesService colorIdentitiesService)
        {
            RuleFor(ci => ci.Id)
                .NotEmpty()
                .MustAsync(async (id, cancellationToken)
                    => await colorIdentitiesService.ExistsAsync(ci => ci.Id == id, cancellationToken))
                .WithMessage((_) => DefaultMessages.MustExistMessage(nameof(ColorIdentity), nameof(ColorIdentity.Id), $"{_.Id}"));
        }
    }
}
