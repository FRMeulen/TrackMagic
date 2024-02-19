using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Dtos;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.Features.Players.Get
{
    public class GetPlayerQuery : IRequest<PlayerDto>
    {
        public int Id { get; set; }
    }

    public class GetPlayerQueryHandler : IRequestHandler<GetPlayerQuery, PlayerDto>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetPlayerQueryHandler(IAppDbContext dbContext, IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<PlayerDto> Handle(GetPlayerQuery request, CancellationToken cancellationToken)
        {
            var player = await _dbContext.Set<Player>()
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            return _mapper.Map<PlayerDto>(player);
        }
    }
}
