using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackMagic.Domain.Entities;
using TrackMagic.Infrastructure.Persistence.Extensions;
using TrackMagic.Shared.Constants;

namespace TrackMagic.Infrastructure.Persistence.Configurations
{
    public class ColorIdentityEntityTypeConfiguration : IEntityTypeConfiguration<ColorIdentity>
    {
        public void Configure(EntityTypeBuilder<ColorIdentity> builder)
        {
            builder.ToTable(TableNames.ColorIdentities)
                .IsIdentifiable();

            builder.Property(ci => ci.Name)
                .IsRequired();

            builder.Property(ci => ci.Colors)
                .IsRequired();
        }
    }
}
