using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Services;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.Features.Contestants
{
    public interface IContestantsService : IFeatureService<Contestant> { }
    public class ContestantsService : BaseFeatureService<Contestant>, IContestantsService
    {
        public ContestantsService(IAppDbContext dbContext) : base(dbContext) { }
    }
}
