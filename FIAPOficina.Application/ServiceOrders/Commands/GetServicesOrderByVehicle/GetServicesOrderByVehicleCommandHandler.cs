using FIAPOficina.Application.Clients.Services;
using FIAPOficina.Application.Vehicles.Services;
using FIAPOficina.Domain.ServiceOrders.Entities;
using FIAPOficina.Domain.ServiceOrders.Repositories;

namespace FIAPOficina.Application.ServiceOrders.Commands.GetServicesOrderByVehicle
{
    internal class GetServicesOrderByVehicleCommandHandler
    {
        private readonly IServiceOrderRepository _repository;
        private readonly IVehiclesService _vehiclesService;
        private readonly IClientsService _clientsService;

        public GetServicesOrderByVehicleCommandHandler(IServiceOrderRepository repository, IVehiclesService vehiclesService, IClientsService clientsService)
        {
            _repository = repository;
            _vehiclesService = vehiclesService;
            _clientsService = clientsService;
        }

        public async Task<ServiceOrder[]> Handle(GetServicesOrderByVehicleCommand command)
        {
            var vehicle = await _vehiclesService.GetSingleAsync(new(command.VehiclePlate));
            if (vehicle is null)
            {
                return Array.Empty<ServiceOrder>();
            }

            var client = await _clientsService.GetSingleAsync(new(command.ClientIdentifier));
            if (client is null || client.Id != vehicle.ClientId)
            {
                return Array.Empty<ServiceOrder>();
            }

            return _repository.GetServicesOrderByVehiclePlate(command.VehiclePlate);
        }
    }
}