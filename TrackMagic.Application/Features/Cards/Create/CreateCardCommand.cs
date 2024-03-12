using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Requests;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Enums;

namespace TrackMagic.Application.Features.Cards.Create
{
    public class CreateCardCommand : ICommand<int>
    {
        public string Name { get; set; } = default!;
        public List<CardTypes> CardTypes { get; set; } = default!;
        public int ColorIdentityId { get; set; }
    }

    public class CreateCardCommandHandler : ICommandHandler<CreateCardCommand, int>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly ILogger<CreateCardCommandHandler> _logger;

        public CreateCardCommandHandler(IAppDbContext dbContext, ILogger<CreateCardCommandHandler> logger)
            => (_appDbContext, _logger) = (dbContext, logger);

        public async Task<int> Handle(CreateCardCommand command, CancellationToken cancellationToken)
        {
            var cardToCreate = new Card
            {
                Name = command.Name,
                CardTypes = command.CardTypes,
                ColorIdentityId = command.ColorIdentityId
            };

            _logger.LogInformation($"Creating card {cardToCreate.Name}.");
            _appDbContext.Set<Card>().Add(cardToCreate);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            var createdCard = await _appDbContext.Set<Card>()
                .Where(c => c.Name == command.Name)
                .FirstAsync(cancellationToken);

            return createdCard.Id;
        }
    }
}
