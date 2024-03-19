using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackMagic.Domain.Entities;
using TrackMagic.Infrastructure.Persistence.Extensions;
using TrackMagic.Shared.Constants;

namespace TrackMagic.Infrastructure.Persistence.Configurations
{
    public class DeckEntityTypeConfiguration : IEntityTypeConfiguration<Deck>
    {
        public void Configure(EntityTypeBuilder<Deck> builder)
        {
            builder.ToTable(TableNames.Decks)
                .IsIdentifiable();

            builder.Property(d => d.Name)
                .IsRequired();

            builder.HasMany(d => d.Commanders)
                .WithMany(c => c.CommanderOf)
                .UsingEntity("DecksCardsJoinTable");

            builder.HasOne(d => d.Companion)
                .WithMany(c => c.CompanionOf)
                .HasForeignKey(d => d.CompanionId)
                .HasConstraintName("FK_Deck_Card_CompanionId");

            builder.HasOne(d => d.Decklist)
                .WithOne(dl => dl.Deck)
                .HasForeignKey<Deck>(d => d.DecklistId)
                .HasConstraintName("FK_Deck_Decklist_DecklistId");
        }
    }
}
