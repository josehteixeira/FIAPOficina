using FIAPOficina.Application.Authentication.Services;
using FIAPOficina.Application.Clients.Services;
using FIAPOficina.Application.Common.Security;
using FIAPOficina.Application.Materials.Services;
using FIAPOficina.Application.ServiceOrders.Services;
using FIAPOficina.Application.Services.Services;
using FIAPOficina.Application.Users.Services;
using FIAPOficina.Application.Vehicles.Services;

namespace FIAPOficina.Api.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IPasswordHasherService, PasswordHasherService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IClientsService, ClientsService>();
            services.AddScoped<IVehiclesService, VehiclesService>();
            services.AddScoped<IServicesService, ServicesService>();
            services.AddScoped<IMaterialsService, MaterialsService>();
            services.AddScoped<IServiceOrderService, ServiceOrderService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
        }
    }
}
