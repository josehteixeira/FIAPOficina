using FIAPOficina.Application.Clients.Services;
using FIAPOficina.Application.Vehicles.Commands.CreateVehicle;
using FIAPOficina.Application.Vehicles.Commands.DeleteVehicle;
using FIAPOficina.Application.Vehicles.Commands.GetAllVehicles;
using FIAPOficina.Application.Vehicles.Commands.GetSingleVehicle;
using FIAPOficina.Application.Vehicles.Commands.UpdateVehicle;
using FIAPOficina.Domain.Vehicles.Entities;
using FIAPOficina.Domain.Vehicles.Repositories;

namespace FIAPOficina.Application.Vehicles.Services
{
    public class VehiclesService : IVehiclesService
    {
        private readonly CreateVehicleCommandHandler _createHandler;
        private readonly UpdateVehicleCommandHandler _updateHandler;
        private readonly DeleteVehicleCommandHandler _deleteHandler;
        private readonly GetSingleVehicleCommandHandler _querySingleHandler;
        private readonly GetAllVehiclesCommandHandler _queryAllHandler;

        public VehiclesService(IVehicleRepository repository, IClientsService clientsService)
        {
            _createHandler = new CreateVehicleCommandHandler(repository, clientsService);
            _updateHandler = new UpdateVehicleCommandHandler(repository);
            _deleteHandler = new DeleteVehicleCommandHandler(repository);
            _querySingleHandler = new GetSingleVehicleCommandHandler(repository);
            _queryAllHandler = new GetAllVehiclesCommandHandler(repository);
        }

        public async Task<Vehicle> AddAsync(CreateVehicleCommand command)
        {
            return await _createHandler.Handle(command).ConfigureAwait(false);
        }

        public async Task<Vehicle> UpdateAsync(UpdateVehicleCommand command)
        {
            return await _updateHandler.Handle(command).ConfigureAwait(false);
        }

        public async Task DeleteAsync(DeleteVehicleCommand command)
        {
            await _deleteHandler.Handle(command).ConfigureAwait(false);
        }

        public async Task<Vehicle?> GetSingleAsync(GetSingleVehicleCommand command)
        {
            return await _querySingleHandler.Handle(command).ConfigureAwait(false);
        }

        public Vehicle[] GetAll(GetAllVehiclesCommand command)
        {
            return _queryAllHandler.Handle(command);
        }
    }
}