using FIAPOficina.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAPOficina.Infrastructure.Database.Mappings
{
    internal class UserMapping : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable<Users>(nameof(Users));
            builder.HasKey(entity => entity.Id);
            builder.Property(entity => entity.Name).IsRequired();
            builder.Property(entity => entity.UserName).IsRequired();
            builder.Property(entity => entity.PasswordHash).IsRequired();
            builder.HasIndex(entity => entity.UserName).IsUnique();
        }
    }
}