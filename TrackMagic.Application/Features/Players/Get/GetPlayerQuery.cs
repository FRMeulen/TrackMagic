using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Requests;
using TrackMagic.Application.Dtos;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Constants;
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

        public GetPlayerQueryHandler(IAppDbContext dbContext, IMapper mapper)
            => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<PlayerDto> Handle(GetPlayerQuery request, CancellationToken cancellationToken)
        {
            throw new ConfigurationException("Configured to fail!");

            var player = await _dbContext.Set<Player>()
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            return _mapper.Map<PlayerDto>(player);
        }
    }
}
