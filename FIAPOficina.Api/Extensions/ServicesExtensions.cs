using FIAPOficina.Application.Common.Security;
using FIAPOficina.Application.Users.Services;

namespace FIAPOficina.Api.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
           services.AddSingleton<IPasswordHasherService, PasswordHasherService>();
           services.AddSingleton<IUsersService, UsersService>();
        }
    }
}
