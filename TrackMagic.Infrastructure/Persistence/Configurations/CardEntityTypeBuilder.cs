using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackMagic.Domain.Entities;
using TrackMagic.Infrastructure.Persistence.Extensions;
using TrackMagic.Shared.Constants;

namespace TrackMagic.Infrastructure.Persistence.Configurations
{
    public class CardEntityTypeBuilder : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.ToTable(TableNames.Cards)
                .IsIdentifiable();

            builder.Property(c => c.Name)
                .IsRequired();

            builder.Property(c => c.CardTypes)
                .IsRequired();

            builder.HasOne(c => c.ColorIdentity)
                .WithMany(ci => ci.CardsInIdentity)
                .HasForeignKey(c => c.ColorIdentityId)
                .HasConstraintName("FK_Card_ColorIdentity_ColorIdentityId");
        }
    }
}
