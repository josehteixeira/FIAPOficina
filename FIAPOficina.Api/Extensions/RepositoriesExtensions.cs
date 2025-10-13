using FIAPOficina.Domain.Users.Repositories;
using FIAPOficina.Infrastructure.Repositories;

namespace FIAPOficina.Api.Extensions
{
    public static class RepositoriesExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IUserRepository, UserRepository>();
        }
    }
}