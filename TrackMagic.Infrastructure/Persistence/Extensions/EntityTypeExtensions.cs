using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrackMagic.Domain.Entities.Base;

namespace TrackMagic.Infrastructure.Persistence.Extensions
{
    public static class EntityTypeExtensions
    {
        public static EntityTypeBuilder<TEntity> IsIdentifiable<TEntity>(this EntityTypeBuilder<TEntity> entity) where TEntity : BaseEntity
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .HasColumnName("Id")
                .IsRequired()
                .UseIdentityColumn();

            return entity;
        }
    }
}
