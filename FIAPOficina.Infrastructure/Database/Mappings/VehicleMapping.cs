using FIAPOficina.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAPOficina.Infrastructure.Database.Mappings
{
    internal class VehicleMapping : IEntityTypeConfiguration<Vehicles>
    {
        public void Configure(EntityTypeBuilder<Vehicles> builder)
        {
            builder.ToTable<Vehicles>(nameof(Vehicles));
            builder.HasKey(entity => entity.Id);
            builder.Property(entity => entity.Brand);
            builder.Property(entity => entity.Model);
            builder.Property(entity => entity.Year);
            builder.Property(entity => entity.Plate);
            builder.Property(entity => entity.Color);
            builder.Property(entity => entity.ClientId);

            builder.HasIndex(entity => entity.Plate).IsUnique();

            builder.HasOne(entity => entity.Client)
                .WithMany(client => client.Vehicles)
                .HasForeignKey(vehicle => vehicle.ClientId);
        }
    }
}