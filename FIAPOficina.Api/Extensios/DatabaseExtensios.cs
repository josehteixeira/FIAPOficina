using FIAPOficina.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace FIAPOficina.Api.Extensios
{
    public static class DatabaseExtensios
    {
        public static void AddDbContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine),
                ServiceLifetime.Singleton
            );
        }
    }
}
