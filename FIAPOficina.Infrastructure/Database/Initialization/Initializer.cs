using FIAPOficina.Application.Common.Security;
using FIAPOficina.Infrastructure.Database.Context;
using FIAPOficina.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FIAPOficina.Infrastructure.Database.Initialization
{
    public class Initializer
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasherService>();

            if (!await context.Users.AnyAsync(u => u.UserName == "admin"))
            {
                var admin = new Users
                {
                    Id = Guid.NewGuid(),
                    Name = "administrator",
                    UserName = "admin",
                    PasswordHash = hasher.Hash("admin"),
                };

                context.Users.Add(admin);
                await context.SaveChangesAsync();
            }
        }
    }
}
