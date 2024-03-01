using System.Linq.Expressions;
using TrackMagic.Domain.Entities.Base;

namespace TrackMagic.Application.Common.Services
{
    public interface IFeatureService<TEntity> where TEntity : BaseEntity
    {
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
    }
}
