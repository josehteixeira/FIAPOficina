using FIAPOficina.Domain.Users.Repositories;
using FIAPOficina.Infrastructure.Repositories;

namespace FIAPOficina.Api.Extensios
{
    public static class RepositoriesExtension
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IUserRepository, UserRepository>();
        }
    }
}