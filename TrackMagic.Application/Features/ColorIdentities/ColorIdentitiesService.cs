using TrackMagic.Application.Common.Persistence;
using TrackMagic.Application.Common.Services;
using TrackMagic.Domain.Entities;

namespace TrackMagic.Application.Features.ColorIdentities
{
    public interface IColorIdentitiesService : IFeatureService<ColorIdentity> { }

    public class ColorIdentitiesService : BaseFeatureService<ColorIdentity>, IColorIdentitiesService
    {
        public ColorIdentitiesService(IAppDbContext dbContext) : base(dbContext) { }
    }
}
