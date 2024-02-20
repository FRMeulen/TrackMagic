using TrackMagic.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace TrackMagic.Infrastructure.Persistence.Initialization
{
    public class DatabaseInitializer
    {
        private readonly AppDbContext _dbContext;

        public DatabaseInitializer(AppDbContext dbContext) => _dbContext = dbContext;

        public async Task InitializeAsync(CancellationToken cancellationToken)
        {
            if (_dbContext.Database.GetMigrations().Any())
            {
                if ((await _dbContext.Database.GetPendingMigrationsAsync(cancellationToken)).Any())
                {
                    await _dbContext.Database.MigrateAsync(cancellationToken);
                }
            }
        }
    }
}
