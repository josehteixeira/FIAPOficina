using FIAPOficina.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAPOficina.Infrastructure.Database.Mappings
{
    internal class ClientMapping : IEntityTypeConfiguration<Clients>
    {
        public void Configure(EntityTypeBuilder<Clients> builder)
        {
            builder.ToTable<Clients>(nameof(Clients));
            builder.HasKey(entity => entity.Id);
            builder.Property(entity => entity.Name);
            builder.Property(entity => entity.Identifier);
            builder.Property(entity => entity.Phone);
            builder.Property(entity => entity.Email);
            builder.Property(entity => entity.Identifier);

            builder.HasIndex(entity => entity.Address).IsUnique();
        }
    }
}