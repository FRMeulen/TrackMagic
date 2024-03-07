using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackMagic.Domain.Entities;
using TrackMagic.Infrastructure.Persistence.Extensions;
using TrackMagic.Shared.Constants;

namespace TrackMagic.Infrastructure.Persistence.Configurations
{
    public class ContestantEntityTypeConfiguration : IEntityTypeConfiguration<Contestant>
    {
        public void Configure(EntityTypeBuilder<Contestant> builder)
        {
            builder.ToTable(TableNames.Contestants)
                .IsIdentifiable();

            builder.Property(c => c.Points)
                .IsRequired();

            builder.HasOne(c => c.Player)
                .WithMany(p => p.Contested)
                .HasForeignKey(c => c.PlayerId)
                .HasConstraintName("FK_Contestant_Player_PlayerId")
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.Deck)
                .WithMany(d => d.PilotedBy)
                .HasForeignKey(c => c.DeckId)
                .HasConstraintName("FK_Contestant_Deck_DeckId");
        }
    }
}
