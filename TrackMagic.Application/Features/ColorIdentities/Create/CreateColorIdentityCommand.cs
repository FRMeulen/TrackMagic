using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Requests;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Enums;

namespace TrackMagic.Application.Features.ColorIdentities.Create
{
    public class CreateColorIdentityCommand : ICommand<int>
    {
        public string Name { get; set; } = default!;
        public List<Colors> Colors { get; set; } = default!;
    }

    public class CreateColorIdentityCommandHandler : ICommandHandler<CreateColorIdentityCommand, int>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly ILogger<CreateColorIdentityCommandHandler> _logger;

        public CreateColorIdentityCommandHandler(IAppDbContext dbContext, ILogger<CreateColorIdentityCommandHandler> logger)
            => (_appDbContext, _logger) = (dbContext, logger);

        public async Task<int> Handle(CreateColorIdentityCommand command, CancellationToken cancellationToken)
        {
            var colorIdentityToCreate = new ColorIdentity
            {
                Name = command.Name,
                Colors = command.Colors
            };

            _logger.LogInformation($"Creating color identity {command.Name}.");
            _appDbContext.Set<ColorIdentity>().Add(colorIdentityToCreate);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            var createdColorIdentity = await _appDbContext.Set<ColorIdentity>()
                .Where(ci => ci.Name == command.Name)
                .FirstAsync(cancellationToken);

            return createdColorIdentity.Id;
        }
    }
}
