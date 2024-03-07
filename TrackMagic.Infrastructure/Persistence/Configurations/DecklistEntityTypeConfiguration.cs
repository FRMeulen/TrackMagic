using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackMagic.Domain.Entities;
using TrackMagic.Infrastructure.Persistence.Extensions;
using TrackMagic.Shared.Constants;

namespace TrackMagic.Infrastructure.Persistence.Configurations
{
    public class DecklistEntityTypeConfiguration : IEntityTypeConfiguration<Decklist>
    {
        public void Configure(EntityTypeBuilder<Decklist> builder)
        {
            builder.ToTable(TableNames.Decklists)
                .IsIdentifiable();

            builder.HasMany(dl => dl.Cards)
                .WithMany(c => c.UsedIn)
                .UsingEntity("DecklistCardsJoinTable");
        }
    }
}
