using Microsoft.EntityFrameworkCore;
using TrackMagic.Application.Common.Persistence;

namespace TrackMagic.Infrastructure.Persistence.Context
{
    public class AppDbContext : DbContextBase, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
