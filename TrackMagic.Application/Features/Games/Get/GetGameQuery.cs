using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Requests;
using TrackMagic.Application.Dtos;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Exceptions;

namespace TrackMagic.Application.Features.Games.Get
{
    public class GetGameQuery : IQuery<GameDto>
    {
        public int Id { get; set; }
    }

    public class GetGameQueryHandler : IQueryHandler<GetGameQuery, GameDto>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<GetGameQueryHandler> _logger;

        public GetGameQueryHandler(IAppDbContext dbContext, IMapper mapper, ILogger<GetGameQueryHandler> logger)
            => (_appDbContext, _mapper, _logger) = (dbContext, mapper, logger);

        public async Task<GameDto> Handle(GetGameQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Fetching game {query.Id}.");
            var game = await _appDbContext.Set<Game>()
                .Include(g => g.Contestants)
                .Where(g => g.Id == query.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (game == null) throw new NotFoundException(nameof(Game));

            return _mapper.Map<GameDto>(game);
        }
    }
}
