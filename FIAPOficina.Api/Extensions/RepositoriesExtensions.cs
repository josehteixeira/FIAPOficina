using FIAPOficina.Domain.Clients.Repositories;
using FIAPOficina.Domain.Materials.Repositories;
using FIAPOficina.Domain.ServiceOrders.Repositories;
using FIAPOficina.Domain.Services.Repositories;
using FIAPOficina.Domain.Users.Repositories;
using FIAPOficina.Domain.Vehicles.Repositories;
using FIAPOficina.Infrastructure.Repositories;

namespace FIAPOficina.Api.Extensions
{
    public static class RepositoriesExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IMaterialRepository, MaterialRepository>();
            services.AddScoped<IServiceOrderRepository, ServiceOrderRepository>();
        }
    }
}