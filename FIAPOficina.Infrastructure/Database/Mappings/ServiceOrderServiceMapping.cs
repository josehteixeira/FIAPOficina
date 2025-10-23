using FIAPOficina.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAPOficina.Infrastructure.Database.Mappings
{
    internal class ServiceOrderServiceMapping : IEntityTypeConfiguration<ServiceOrderServices>
    {
        public void Configure(EntityTypeBuilder<ServiceOrderServices> builder)
        {
            builder.ToTable<ServiceOrderServices>(nameof(ServiceOrderServices));
            builder.HasKey(entity => entity.Id);
            builder.Property(entity => entity.Quantity);
            builder.Property(entity => entity.Value);
            builder.Property(entity => entity.ServiceId);
            builder.Property(entity => entity.ServiceOrderId);

            builder.HasIndex(entity => new { entity.ServiceOrderId, entity.ServiceId })
                    .IsUnique();

            builder.HasOne(entity => entity.Service)
                    .WithMany(service => service.ServiceOrderServices)
                    .HasForeignKey(service => service.ServiceId);

            builder.HasOne(entity => entity.ServiceOrder)
                    .WithMany(service => service.Services)
                    .HasForeignKey(service => service.ServiceOrderId);
        }
    }
}
