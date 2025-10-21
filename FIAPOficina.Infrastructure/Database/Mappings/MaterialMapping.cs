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
            
            builder.HasMany(entity => entity.ServiceOrderMaterials)
                .WithOne(serviceOrderMaterials => serviceOrderMaterials.Materials)
                .HasForeignKey(serviceOrderMaterials => serviceOrderMaterials.MaterialId);

        }
    }
}
