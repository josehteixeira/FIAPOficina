using FIAPOficina.Api.Extensions;
using FIAPOficina.Infrastructure.Mail;

namespace FIAPOficina
{
    public class Program
    {
        public static void Main(string[] args)
        {
            sendEmailTeste();
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddServices();
            builder.Services.AddRepositories();
            builder.AddDbContext();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.ApplyMigrations();
            app.CreateInitialRecords().GetAwaiter().GetResult();
            app.Run();
        }

        private static void sendEmailTeste()
        {
            var mailSv = new MailService("","");
            mailSv.SendMail( mailSv.CreateMailMessage("", "", true));
        }
    }
}