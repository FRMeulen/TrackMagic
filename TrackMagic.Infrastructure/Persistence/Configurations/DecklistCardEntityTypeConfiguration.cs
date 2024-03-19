using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackMagic.Domain.Entities;
using TrackMagic.Infrastructure.Persistence.Extensions;
using TrackMagic.Shared.Constants;

namespace TrackMagic.Infrastructure.Persistence.Configurations
{
    public class DecklistCardEntityTypeConfiguration : IEntityTypeConfiguration<DecklistCard>
    {
        public void Configure(EntityTypeBuilder<DecklistCard> builder)
        {
            builder.ToTable(TableNames.DecklistCards)
                .IsIdentifiable();

            builder.Property(dlc => dlc.Amount)
                .HasDefaultValue(1);
        }
    }
}
