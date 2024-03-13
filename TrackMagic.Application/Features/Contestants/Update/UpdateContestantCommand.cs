using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Requests;
using TrackMagic.Application.Dtos;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.Features.Contestants.Update
{
    public class UpdateContestantCommand : ICommand<ContestantDto>
    {
        public int Id { get; set; }
        public int Points { get; set; }
        public int GameId { get; set; }
        public int PlayerId { get; set; }
        public int DeckId { get; set; }
    }

    public class UpdateContestantCommandHandler : ICommandHandler<UpdateContestantCommand, ContestantDto>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateContestantCommandHandler> _logger;

        public UpdateContestantCommandHandler(IAppDbContext dbContext, IMapper mapper, ILogger<UpdateContestantCommandHandler> logger)
            => (_appDbContext, _mapper, _logger) = (dbContext, mapper, logger);

        public async Task<ContestantDto> Handle(UpdateContestantCommand command, CancellationToken cancellationToken)
        {
            var contestantToUpdate = await _appDbContext.Set<Contestant>()
                .Where(c => c.Id == command.Id)
                .FirstAsync(cancellationToken);

            contestantToUpdate.Points = command.Points;
            contestantToUpdate.GameId = command.GameId;
            contestantToUpdate.PlayerId = command.PlayerId;
            contestantToUpdate.DeckId = command.DeckId;

            _logger.LogInformation($"Updating contestant {command.Id}.");
            _appDbContext.Set<Contestant>().Update(contestantToUpdate);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ContestantDto>(contestantToUpdate);
        }
    }
}
