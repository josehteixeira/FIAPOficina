using FIAPOficina.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAPOficina.Infrastructure.Database.Mappings
{
    internal class ServiceMapping : IEntityTypeConfiguration<Services>
    {
        public void Configure(EntityTypeBuilder<Services> builder)
        {
            builder.ToTable<Services>(nameof(Services));
            builder.HasKey(entity => entity.Id);
            builder.Property(entity => entity.Name);
            builder.Property(entity => entity.Description);
            builder.Property(entity => entity.Value);
        }
    }
}