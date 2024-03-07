using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackMagic.Domain.Entities;
using TrackMagic.Infrastructure.Persistence.Extensions;
using TrackMagic.Shared.Constants;

namespace TrackMagic.Infrastructure.Persistence.Configurations
{
    public class GameEntityTypeConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.ToTable(TableNames.Games)
                .IsIdentifiable();

            builder.Property(g => g.LengthInCycles)
                .IsRequired();

            builder.Property(g => g.Date)
                .IsRequired();

            builder.Property(g => g.GameType)
                .IsRequired();

            builder.HasMany(g => g.Contestants)
                .WithOne(c => c.Game)
                .HasForeignKey(c => c.GameId)
                .HasConstraintName("FK_Game_Contestant_GameId");
        }
    }
}
