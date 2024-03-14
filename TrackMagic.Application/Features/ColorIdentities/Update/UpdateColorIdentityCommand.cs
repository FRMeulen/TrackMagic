using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Requests;
using TrackMagic.Application.Dtos;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Enums;

namespace TrackMagic.Application.Features.ColorIdentities.Update
{
    public class UpdateColorIdentityCommand : ICommand<ColorIdentityDto>
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public List<Colors> Colors { get; set; } = default!;
    }

    public class UpdateColorIdentityCommandHandler : ICommandHandler<UpdateColorIdentityCommand, ColorIdentityDto>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateColorIdentityCommandHandler> _logger;

        public UpdateColorIdentityCommandHandler(IAppDbContext dbContext, IMapper mapper, ILogger<UpdateColorIdentityCommandHandler> logger)
            => (_appDbContext, _mapper, _logger) = (dbContext, mapper, logger);

        public async Task<ColorIdentityDto> Handle(UpdateColorIdentityCommand command, CancellationToken cancellationToken)
        {
            var colorIdentityToUpdate = await _appDbContext.Set<ColorIdentity>()
                .Include(c => c.CardsInIdentity)
                .Where(ci => ci.Id == command.Id)
                .FirstAsync(cancellationToken);

            colorIdentityToUpdate.Name = command.Name;
            colorIdentityToUpdate.Colors = command.Colors;

            _logger.LogInformation($"Updating color identity {command.Name}.");
            _appDbContext.Set<ColorIdentity>().Update(colorIdentityToUpdate);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ColorIdentityDto>(colorIdentityToUpdate);
        }
    }
}
