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
    internal class ServiceOrderServiceMapping : IEntityTypeConfiguration<ServiceOrderServices>
    {
        public void Configure(EntityTypeBuilder<ServiceOrderServices> builder)
        {
            builder.ToTable<ServiceOrderServices>(nameof(ServiceOrderServices));
            builder.HasKey(entity => new { entity.ServiceOrderId, entity.ServiceId });
            builder.Property(entity => entity.Quantity);
            builder.Property(entity => entity.UnitValue);
            builder.Property(entity => entity.TotalValue);

            builder.HasOne(entity => entity.Serices)
                .WithMany(service => service.ServiceOrderServices)
                .HasForeignKey(service => service.ServiceId);

            builder.HasOne(entity => entity.ServiceOrder)
                .WithMany(service => service.Services)
                .HasForeignKey(service => service.ServiceOrderId);
        }
    }
}
