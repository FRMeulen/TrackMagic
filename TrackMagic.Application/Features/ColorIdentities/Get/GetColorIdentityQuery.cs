using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Requests;
using TrackMagic.Application.Dtos;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Exceptions;

namespace TrackMagic.Application.Features.ColorIdentities.Get
{
    public class GetColorIdentityQuery : IQuery<ColorIdentityDto>
    {
        public int Id { get; set; }
    }

    public class GetColorIdentityQueryHandler : IQueryHandler<GetColorIdentityQuery, ColorIdentityDto>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<GetColorIdentityQueryHandler> _logger;

        public GetColorIdentityQueryHandler(IAppDbContext dbContext, IMapper mapper, ILogger<GetColorIdentityQueryHandler> logger)
            => (_appDbContext, _mapper, _logger) = (dbContext, mapper, logger);

        public async Task<ColorIdentityDto> Handle(GetColorIdentityQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Fetching color identity {query.Id}.");
            var colorIdentity = await _appDbContext.Set<ColorIdentity>()
                .Where(ci => ci.Id == query.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (colorIdentity == null) throw new NotFoundException(nameof(ColorIdentity));

            return _mapper.Map<ColorIdentityDto>(colorIdentity);
        }
    }
}
