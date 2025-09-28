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
            builder.HasKey(user => user.Id);
            builder.Property(user => user.Name).IsRequired();
            builder.Property(user => user.UserName).IsRequired();
            builder.Property(user => user.PasswordHash).IsRequired();
            builder.HasIndex(user => user.UserName).IsUnique();
        }
    }
}