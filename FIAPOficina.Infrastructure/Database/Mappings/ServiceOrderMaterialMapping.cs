using FIAPOficina.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAPOficina.Infrastructure.Database.Mappings
{
    internal class ServiceOrderMaterialMapping : IEntityTypeConfiguration<ServiceOrderMaterials>
    {
        public void Configure(EntityTypeBuilder<ServiceOrderMaterials> builder)
        {
            builder.ToTable<ServiceOrderMaterials>(nameof(ServiceOrderMaterials));
            builder.HasKey(entity => entity.Id);
            builder.Property(entity => entity.Quantity);
            builder.Property(entity => entity.Value);
            builder.Property(entity => entity.MaterialId);
            builder.Property(entity => entity.ServiceOrderId);

            builder.HasIndex(entity => new { entity.ServiceOrderId, entity.MaterialId })
                    .IsUnique();

            builder.HasOne(entity => entity.Material)
                    .WithMany(service => service.ServiceOrderMaterials)
                    .HasForeignKey(service => service.MaterialId);

            builder.HasOne(entity => entity.ServiceOrder)
                    .WithMany(service => service.Materials)
                    .HasForeignKey(service => service.ServiceOrderId);
        }
    }
}
