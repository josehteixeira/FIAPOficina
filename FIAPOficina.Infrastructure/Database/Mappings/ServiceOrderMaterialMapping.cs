using FIAPOficina.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAPOficina.Infrastructure.Database.Mappings
{
    internal class ServiceOrderMaterialMapping : IEntityTypeConfiguration<ServiceOrderMaterials>
    {
        public void Configure(EntityTypeBuilder<ServiceOrderMaterials> builder)
        {
            builder.ToTable<ServiceOrderMaterials>(nameof(ServiceOrderMaterials));
            builder.HasKey(entity => new { entity.ServiceOrderId, entity.MaterialId });
            builder.Property(entity => entity.Quantity);
            builder.Property(entity => entity.UnitValue);
            builder.Property(entity => entity.TotalValue);

            builder.HasOne(entity => entity.Materials)
                   .WithMany(service => service.ServiceOrderMaterials)
                   .HasForeignKey(service => service.MaterialId);

            builder.HasOne(entity => entity.ServiceOrder)
                .WithMany(service => service.Materials)
                .HasForeignKey(service => service.ServiceOrderId);


        }
    }
}
