using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Requests;
using TrackMagic.Application.Dtos;
using TrackMagic.Domain.Entities;
using TrackMagic.Shared.Enums;

namespace TrackMagic.Application.Features.Cards.Update
{
    public class UpdateCardCommand : ICommand<CardDto>
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public List<CardTypes> CardTypes { get; set; } = default!;
        public int ColorIdentityId { get; set; }
    }

    public class UpdateCardCommandHandler : ICommandHandler<UpdateCardCommand, CardDto>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCardCommandHandler> _logger;

        public UpdateCardCommandHandler(IAppDbContext dbContext, IMapper mapper, ILogger<UpdateCardCommandHandler> logger)
            => (_appDbContext, _mapper, _logger) = (dbContext, mapper, logger);

        public async Task<CardDto> Handle(UpdateCardCommand command, CancellationToken cancellationToken)
        {
            var cardToUpdate = await _appDbContext.Set<Card>()
                .Include(c => c.ColorIdentity)
                .Include(c => c.CommanderOf)
                .Include(c => c.CompanionOf)
                .Include(c => c.UsedIn)
                .Where(c => c.Id == command.Id)
                .FirstAsync(cancellationToken);

            cardToUpdate.Name = command.Name;
            cardToUpdate.CardTypes = command.CardTypes;
            cardToUpdate.ColorIdentityId = command.ColorIdentityId;

            _logger.LogInformation($"Updating card {command.Name}.");
            _appDbContext.Set<Card>().Update(cardToUpdate);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CardDto>(cardToUpdate);
        }
    }
}
