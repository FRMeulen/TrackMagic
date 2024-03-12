using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Requests;
using TrackMagic.Application.Dtos;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Exceptions;

namespace TrackMagic.Application.Features.Players.Get
{
    public class GetPlayerQuery : IQuery<PlayerDto>
    {
        public int Id { get; set; }
    }

    public class GetPlayerQueryHandler : IQueryHandler<GetPlayerQuery, PlayerDto>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPlayerQueryHandler> _logger;

        public GetPlayerQueryHandler(IAppDbContext dbContext, IMapper mapper, ILogger<GetPlayerQueryHandler> logger)
            => (_dbContext, _mapper, _logger) = (dbContext, mapper, logger);

        public async Task<PlayerDto> Handle(GetPlayerQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Fetching player {request.Id}.");
            var player = await _dbContext.Set<Player>()
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (player == null) throw new NotFoundException(nameof(Player));

            return _mapper.Map<PlayerDto>(player);
        }
    }
}
