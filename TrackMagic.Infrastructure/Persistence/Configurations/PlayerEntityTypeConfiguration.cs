using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackMagic.Domain.Entities;
using TrackMagic.Infrastructure.Persistence.Extensions;
using TrackMagic.Shared.Constants;

namespace TrackMagic.Infrastructure.Persistence.Configurations
{
    public class PlayerEntityTypeConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.ToTable(TableNames.Players)
                .IsIdentifiable();

            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
