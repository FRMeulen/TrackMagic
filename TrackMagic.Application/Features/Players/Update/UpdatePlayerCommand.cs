using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Requests;
using TrackMagic.Application.Dtos;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.Features.Players.Update
{
    public class UpdatePlayerCommand : ICommand<PlayerDto>
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
    }

    public class UpdatePlayerCommandHandler : ICommandHandler<UpdatePlayerCommand, PlayerDto>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdatePlayerCommandHandler> _logger;

        public UpdatePlayerCommandHandler(IAppDbContext dbContext, IMapper mapper, ILogger<UpdatePlayerCommandHandler> logger)
            => (_appDbContext, _mapper, _logger) = (dbContext, mapper, logger);

        public async Task<PlayerDto> Handle(UpdatePlayerCommand command, CancellationToken cancellationToken)
        {
            var playerToUpdate = await _appDbContext.Set<Player>()
                .Where(x => x.Id == command.Id)
                .FirstAsync();

            playerToUpdate.FirstName = command.FirstName;
            playerToUpdate.LastName = command.LastName;

            _appDbContext.Set<Player>().Update(playerToUpdate);
            await _appDbContext.SaveChangesAsync();

            return _mapper.Map<PlayerDto>(playerToUpdate);
        }
    }
}
