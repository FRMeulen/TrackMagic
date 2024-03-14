using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Requests;
using TrackMagic.Application.Dtos;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Enums;

namespace TrackMagic.Application.Features.Games.Update
{
    public class UpdateGameCommand : ICommand<GameDto>
    {
        public int Id { get; set; }
        public DateTimeOffset? Date { get; set; }
        public int LengthInCycles { get; set; }
        public GameTypes GameType { get; set; }
    }

    public class UpdateGameCommandHandler : ICommandHandler<UpdateGameCommand, GameDto>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateGameCommandHandler> _logger;

        public UpdateGameCommandHandler(IAppDbContext dbContext, IMapper mapper, ILogger<UpdateGameCommandHandler> logger)
            => (_appDbContext, _mapper, _logger) = (dbContext, mapper, logger);

        public async Task<GameDto> Handle(UpdateGameCommand command, CancellationToken cancellationToken)
        {
            var gameToUpdate = await _appDbContext.Set<Game>()
                .Include(g => g.Contestants)
                .Where(g => g.Id == command.Id)
                .FirstAsync(cancellationToken);

            gameToUpdate.Date = command.Date ?? gameToUpdate.Date;
            gameToUpdate.LengthInCycles = command.LengthInCycles;
            gameToUpdate.GameType = command.GameType;

            _logger.LogInformation($"Updating game {command.Id}.");
            _appDbContext.Set<Game>().Update(gameToUpdate);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<GameDto>(gameToUpdate);
        }
    }
}
