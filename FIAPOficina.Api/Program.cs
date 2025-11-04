using FIAPOficina.Api.Extensions;

namespace FIAPOficina
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.AddAuthenticationConfig();
            builder.Services.AddServices();
            builder.Services.AddRepositories();
            builder.AddDbContext();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthentication();
            app.UseAuthorization();
            app.AddMiddlewares();
            app.MapControllers();
            app.ApplyMigrations();
            app.CreateInitialRecords().GetAwaiter().GetResult();

            app.Run();
        }
    }
}