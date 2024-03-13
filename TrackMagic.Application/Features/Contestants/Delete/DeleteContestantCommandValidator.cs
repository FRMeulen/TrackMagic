using FluentValidation;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Constants;

namespace TrackMagic.Application.Features.Contestants.Delete
{
    public class DeleteContestantCommandValidator : AbstractValidator<DeleteContestantCommand>
    {
        public DeleteContestantCommandValidator(IContestantsService contestantsService)
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                .MustAsync(async (id, cancellationToken)
                    => await contestantsService.ExistsAsync(c => c.Id == id, cancellationToken))
                .WithMessage((_) => DefaultMessages.MustExistMessage(nameof(Contestant), nameof(Contestant.Id), $"{_.Id}"));
        }
    }
}
