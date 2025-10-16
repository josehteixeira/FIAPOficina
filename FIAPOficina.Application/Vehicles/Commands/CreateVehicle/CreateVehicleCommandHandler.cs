using FIAPOficina.Application.Clients.Services;
using FIAPOficina.Domain.Vehicles.Entities;
using FIAPOficina.Domain.Vehicles.Repositories;

namespace FIAPOficina.Application.Vehicles.Commands.CreateVehicle
{
    internal class CreateVehicleCommandHandler
    {
        private readonly IVehicleRepository _repository;
        private readonly IClientsService _clientsService;

        public CreateVehicleCommandHandler(IVehicleRepository repository, IClientsService clientsService)
        {
            _repository = repository;
            _clientsService = clientsService;
        }

        public async Task<Vehicle> Handle(CreateVehicleCommand command)
        {
            var client = await _clientsService.GetSingleAsync(new(command.ClientId)).ConfigureAwait(false);

            if (client is null)
            {
                throw new Exception("Client not found");
            }

            var vehicle = await _repository.AddAsync(
                new(command.Brand, command.Model, command.Year, command.Plate, command.Color, command.ClientId)
            );

            return vehicle;
        }
    }
}