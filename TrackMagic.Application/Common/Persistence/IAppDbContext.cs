using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TrackMagic.Domain.Contracts;

namespace TrackMagic.Application.Common.Persistence
{
    public interface IAppDbContext
    {
        DatabaseFacade Database { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        DbSet<T> Set<T>() where T : class;
    }
}
