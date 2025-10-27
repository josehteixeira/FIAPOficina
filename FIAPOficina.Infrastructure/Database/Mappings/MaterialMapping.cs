using FIAPOficina.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAPOficina.Infrastructure.Database.Mappings
{
    internal class MaterialMapping : IEntityTypeConfiguration<Materials>
    {
        public void Configure(EntityTypeBuilder<Materials> builder)
        {
            builder.ToTable<Materials>(nameof(Materials));
            builder.HasKey(entity => entity.Id);
            builder.Property(entity => entity.Name);
            builder.Property(entity => entity.Value);
            builder.Property(entity => entity.Brand);
            builder.Property(entity => entity.Description);
            builder.Property(entity => entity.Quantity);
            
            builder.HasMany(entity => entity.ServiceOrderMaterials)
                .WithOne(serviceOrderMaterials => serviceOrderMaterials.Material)
                .HasForeignKey(serviceOrderMaterials => serviceOrderMaterials.MaterialId);
        }
    }
}
