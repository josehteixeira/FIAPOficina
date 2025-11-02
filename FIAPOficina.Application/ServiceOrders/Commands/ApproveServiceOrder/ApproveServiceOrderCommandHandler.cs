using FIAPOficina.Application.Clients.Services;
using FIAPOficina.Application.Vehicles.Services;
using FIAPOficina.Domain.Clients.Entities;
using FIAPOficina.Domain.ServiceOrders.Entities;
using FIAPOficina.Domain.ServiceOrders.Repositories;
using FIAPOficina.Domain.Vehicles.Entities;

namespace FIAPOficina.Application.ServiceOrders.Commands.ApproveServiceOrder
{
    internal class ApproveServiceOrderCommandHandler
    {
        private readonly IServiceOrderRepository _repository;
        private readonly IVehiclesService _vehiclesService;
        private readonly IClientsService _clientsService;

        public ApproveServiceOrderCommandHandler(
            IServiceOrderRepository repository, IVehiclesService vehiclesService, IClientsService clientsService)
        {
            _repository = repository;
            _vehiclesService = vehiclesService;
            _clientsService = clientsService;
        }

        public async Task Handle(ApproveServiceOrderCommand command)
        {
            var serviceOrder = await GetServiceOrder(command.ServiceOrderId);

            var vehicle = await GetVehicle(serviceOrder.VehicleId);
            if (vehicle is null || vehicle.Plate != command.VehiclePlate)
            {
                throw new Exception("The info provided does not match the service order!");
            }

            var client = await GetClient(vehicle.ClientId);
            if (client is null || client.Identifier != command.ClientIdentifier)
            {
                throw new Exception("The info provided does not match the service order!");
            }

            if (serviceOrder.Status != ServiceOrderStatus.WaitingApproval)
            {
                throw new Exception("The service order is not waiting for approval!");
            }

            serviceOrder.Status = ServiceOrderStatus.Approved;
            serviceOrder.ApprovedOn = DateTime.Now;
            await _repository.UpdateAsync(serviceOrder).ConfigureAwait(false);
        }

        private async Task<Client?> GetClient(Guid clientId)
        {
            return await _clientsService.GetSingleAsync(new(clientId)).ConfigureAwait(false);
        }

        private async Task<Vehicle?> GetVehicle(Guid vehicleId)
        {
            return await _vehiclesService.GetSingleAsync(new(vehicleId)).ConfigureAwait(false);
        }

        private async Task<ServiceOrder> GetServiceOrder(Guid id)
        {
            var oldServiceOrder = await _repository.FirstOrDefaultAsync(id).ConfigureAwait(false);

            if (oldServiceOrder is null)
            {
                throw new Exception("Service order not found!");
            }

            return oldServiceOrder;
        }
    }
}