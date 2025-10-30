using FIAPOficina.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAPOficina.Infrastructure.Database.Mappings
{
    internal class ServiceMapping : IEntityTypeConfiguration<Services>
    {
        public void Configure(EntityTypeBuilder<Services> builder)
        {
            builder.ToTable(nameof(Services));
            builder.HasKey(entity => entity.Id);
            builder.Property(entity => entity.Name);
            builder.Property(entity => entity.Description);
            builder.Property(entity => entity.Value);

            builder.HasMany(entity => entity.ServiceOrderServices)
                .WithOne(serviceOrderMaterials => serviceOrderMaterials.Service)
                .HasForeignKey(serviceOrderMaterials => serviceOrderMaterials.ServiceId);
        }
    }
}