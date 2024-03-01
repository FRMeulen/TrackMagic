using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TrackMagic.Application.Common.Persistence;
using TrackMagic.Domain.Entities.Base;

namespace TrackMagic.Application.Common.Services
{
    public abstract class BaseFeatureService<TEntity> : IFeatureService<TEntity> where TEntity : BaseEntity
    {
        protected readonly IAppDbContext _dbContext;

        public BaseFeatureService(IAppDbContext dbContext) => _dbContext = dbContext;

        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<TEntity>().AnyAsync(expression, cancellationToken);
        }
    }
}
