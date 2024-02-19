using Microsoft.EntityFrameworkCore;

namespace TrackMagic.Infrastructure.Persistence.Context
{
    public abstract class DbContextBase : DbContext
    {
        public DbContextBase(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
