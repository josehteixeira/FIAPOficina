using FIAPOficina.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAPOficina.Infrastructure.Database.Mappings
{
    internal class ServiceOrderMapping : IEntityTypeConfiguration<ServiceOrders>
    {
        public void Configure(EntityTypeBuilder<ServiceOrders> builder)
        {
            builder.ToTable(nameof(ServiceOrders));
            builder.HasKey(entity => entity.Id);
            builder.Property(entity => entity.Status);
            builder.Property(entity => entity.VehicleId);
            builder.Property(entity => entity.CreatedOn).IsRequired(true);
            builder.Property(entity => entity.ApprovedOn).IsRequired(false);
            builder.Property(entity => entity.FinishedOn).IsRequired(false);

            builder.HasOne(entity => entity.Vehicle)
                .WithMany(vehicle => vehicle.ServiceOrders)
                .HasForeignKey(serviceOrder => serviceOrder.VehicleId);

            builder.HasMany(entity => entity.Services)
                .WithOne(serviceOrderServices => serviceOrderServices.ServiceOrder)
                .HasForeignKey(serviceOrder => serviceOrder.ServiceOrderId);

            builder.HasMany(entity => entity.Materials)
                .WithOne(serviceOrderMaterial => serviceOrderMaterial.ServiceOrder)
                .HasForeignKey(serviceOrder => serviceOrder.ServiceOrderId);
        }
    }
}
