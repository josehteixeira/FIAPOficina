using FIAPOficina.Infrastructure.Database.Context;
using FIAPOficina.Infrastructure.Database.Initialization;
using Microsoft.EntityFrameworkCore;

namespace FIAPOficina.Api.Extensions
{
    public static class DatabaseExtensions
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

        public static void ApplyMigrations(this IHost app)
        {
            using var scope = app.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.Migrate();
        }

        public static async Task CreateInitialRecords(this IHost app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                await Initializer.InitializeAsync(services);
            }
        }
    }
}
